using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using VSOSH.Contracts;
using VSOSH.Dal;
using VSOSH.Dal.Parser;
using VSOSH.Dal.Repositories;
using VSOSH.Dal.Services;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;

namespace VSOSH.UI;

public static class ServiceProviderFactory
{
	#region Properties
	private static IConfiguration Configuration
	{
		get;
		set;
	} = null!;

	private static ILogger Log
	{
		get;
		set;
	}

	private static IServiceProvider ServiceProvider
	{
		get;
		set;
	}
	#endregion

	#region Public
	public static IServiceProvider CreateServiceProvider()
	{
		if (!Directory.Exists(ProfileLocationStorage.ServiceFiles))
		{
			Directory.CreateDirectory(ProfileLocationStorage.ServiceFiles);
			Directory.CreateDirectory(ProfileLocationStorage.ConfigDirPath);
		}

		var services = new ServiceCollection();

		services.AddSerilog();
		Serilog.Log.Logger = SwitchableLogger.Instance.Logger = SwitchableLogger.Instance;
		SwitchableLogger.Instance.Logger = GetBaseLoggerConfig()
			.CreateLogger();
		Log = Serilog.Log.ForContext(typeof(Program));
		if (!File.Exists(ProfileLocationStorage.ConfigPath))
		{
			Log.Error("Не найден файл конфигурации.");
			throw new ApplicationException("Не найден файл конфигурации.");
		}

		var builder = new ConfigurationBuilder().SetBasePath(ProfileLocationStorage.ConfigDirPath)
												.AddJsonFile(ProfileLocationStorage.ConfigFileName, optional: false, reloadOnChange: true);
		Configuration = builder.Build();
		
		services.AddSingleton(Configuration);


		services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Database")));

		services.AddScoped<IParser, ResultParser>();
		services.AddScoped<IResultRepository, ResultRepository>();
		services.AddScoped<IPassingPointsService, PassingPointsService>();
		services.AddScoped<IGeneralReportService, GeneralReportService>();
		services.AddScoped<IQuantitativeDataService, QuantitativeDataService>();
		services.AddScoped<IGreaterClassService, GreaterClassService>();
		ServiceProvider = services.BuildServiceProvider();
		using (var scope = ServiceProvider.CreateScope())
		{
			Log.Information("Начало миграции.");
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			dbContext.Database.Migrate();
			Log.Information("Миграция успешно применилась.");
		}

		services.AddTransient<MainWindow>();

		return services.BuildServiceProvider();
	}

	private static LoggerConfiguration GetBaseLoggerConfig()
	{
		var path = ProfileLocationStorage.LogDirPath;
		Directory.CreateDirectory(path);

		return BaseLoggerConfigurationProvider.GetBaseLoggerConfig("GamesLibraryService", true);
	}
	#endregion
}
