namespace VSOSH.Contracts;

public static class ProfileLocationStorage
{
	#region Data
	#region Static
	/// <summary>
	/// Путь до файлов проекта.
	/// </summary>
	public static string ServiceFiles => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VSOSH");

	/// <summary>
	/// Путь до директории с логами.
	/// </summary>
	public static string LogDirPath = Path.Combine(ServiceFiles, "Logs");

	/// <summary>
	/// Возвращает путь, где будет конфигурация приложения.
	/// </summary>
	public static string ConfigDirPath => Path.Combine(ServiceFiles, "Configuration");
	
	/// <summary>Возвращает имя файла, в котором будет содержаться конфигурация приложения.</summary>
	/// <value>Имя файла, в котором будет содержаться конфигурация приложения.</value>
	public static string ConfigFileName => "configuration.json";

	/// <summary>Возвращает путь, где будут хранится конфигурационные файлы.</summary>
	/// <value>Путь, где будут хранится конфигурационные файлы.</value>
	public static string ConfigPath => Path.Combine(ConfigDirPath, ConfigFileName);

	#endregion
	#endregion
}
