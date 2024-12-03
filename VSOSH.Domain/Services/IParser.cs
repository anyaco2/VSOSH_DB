namespace VSOSH.Domain.Services;

public interface IParser
{
    /// <summary>
    /// Парсит результаты и сохраняет в базу данных.
    /// </summary>
    /// <param name="file">Excel-файл.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns></returns>
    Task ParseAndSaveAsync(Stream file, CancellationToken cancellationToken = default);
}