using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    private static IServiceProvider ServiceProvider { get; set; }
    
    private static IConfiguration Configuration { get; set; } = null!;

    public static IServiceProvider CreateServiceProvider()
    {
        var d = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

        
        if (!Directory.Exists(ProfileLocationStorage.ServiceFiles))
        {
            Directory.CreateDirectory(ProfileLocationStorage.ServiceFiles);
        }
        
        var services = new ServiceCollection();
        
        services.AddSingleton(Configuration);
        services.AddLogging();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Database")));
        
        services.AddScoped<IParser, ResultParser>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IPassingPointsService, PassingPointsService>();
        services.AddScoped<IGeneralReportService, GeneralReportService>();
        services.AddScoped<IQuantitativeDataService, QuantitativeDataService>();
        services.AddScoped<IGreaterClassService, GreaterClassService>();
        ServiceProvider = services.BuildServiceProvider();
        using (var scope = ServiceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        services.AddTransient<MainWindow>();
        
        return services.BuildServiceProvider();
    }
}