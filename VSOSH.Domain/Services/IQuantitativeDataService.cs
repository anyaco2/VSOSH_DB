namespace VSOSH.Domain.Services;

/// <summary>
/// Представляет интерфейс сервиса количественных данных.
/// </summary>
public interface IQuantitativeDataService
{
	#region Overridable
	/// <summary>
	/// Возввращает количественные данные excel-файлом.
	/// </summary>
	/// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
	/// <returns>Количественные данные excel-файлом.</returns>
	Task<FileStream> GetQuantitativeData(CancellationToken cancellationToken = default);
	#endregion
}
