namespace VSOSH.Domain.Services;

public interface IGeneralReportService
{
    /// <summary>
    /// Возвращает общий отчет в excel-файле.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Общий отчет в excel-файле.</returns>
    Task<FileStream> GetGeneralReport(CancellationToken cancellationToken = default);
}