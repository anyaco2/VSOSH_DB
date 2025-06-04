using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using VSOSH.Contracts;

namespace VSOSH.UI;

/// <summary>Базовый провайдер конфигурации логирования Serilog.</summary>
public static class BaseLoggerConfigurationProvider
{
	#region Data
	#region Consts
	/// <summary>Шаблон логов Serilog по-умолчанию.</summary>
	private const string DefaultLogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
	/// <summary>Шаблон логов Serilog, чтобы он выводил время в UTC.</summary>
	private const string UtcLogTemplate = "{Timestamp:u} [{Level:u3}] {Message}{NewLine}{Exception}";
	#endregion
	#endregion

	#region Properties
	/// <summary>Возвращает уровень логгирования.</summary>
	/// <value>Уровень логгирования.</value>
	public static LoggingLevelSwitch LoggingLevel
	{
		get;
	} = new(LogEventLevel.Debug);
	#endregion

	#region Public
	/// <summary>Возвращает конфигурацию логирования приложения.</summary>
	/// <param name="serviceName">Имя сервиса, для которого получается конфигурация логирования.</param>
	/// <param name="useUtcInLogging">Нужно ли использовать UTC в логах Serilog.</param>
	/// <returns>Конфигурация логирования приложения.</returns>
	public static LoggerConfiguration GetBaseLoggerConfig(string serviceName, bool useUtcInLogging)
	{
		const int logFileSizeLimit = 10 * 1024 * 1024;
		var path = ProfileLocationStorage.LogDirPath;

		var logTemplate = useUtcInLogging ? UtcLogTemplate : DefaultLogTemplate;

		return new LoggerConfiguration().MinimumLevel.ControlledBy(LoggingLevel)
										.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
										.MinimumLevel.Override("System", LogEventLevel.Error)
										.Enrich.FromLogContext()
										.WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(e => e.Level <= LogEventLevel.Debug)
																.WriteTo.File(Path.Combine(path, $"{serviceName}.dbg_.log"),
																			  rollOnFileSizeLimit: true,
																			  rollingInterval: RollingInterval.Day,
																			  fileSizeLimitBytes: logFileSizeLimit,
																			  outputTemplate: logTemplate))
										.WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
																.WriteTo.File(Path.Combine(path, $"{serviceName}.trc_.log"),
																			  rollOnFileSizeLimit: true,
																			  rollingInterval: RollingInterval.Day,
																			  fileSizeLimitBytes: logFileSizeLimit,
																			  outputTemplate: logTemplate))
										.WriteTo.File(Path.Combine(path, $"{serviceName}.err_.log"),
													  rollOnFileSizeLimit: true,
													  rollingInterval: RollingInterval.Day,
													  fileSizeLimitBytes: logFileSizeLimit,
													  restrictedToMinimumLevel: LogEventLevel.Error,
													  outputTemplate: logTemplate)
										.WriteTo.File(Path.Combine(path, $"{serviceName}_.log"),
													  rollOnFileSizeLimit: true,
													  rollingInterval: RollingInterval.Day,
													  fileSizeLimitBytes: logFileSizeLimit,
													  restrictedToMinimumLevel: LogEventLevel.Debug,
													  outputTemplate: logTemplate);
	}
	#endregion
}
