using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using VSOSH.Contracts;
using VSOSH.Contracts.Exceptions;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;

namespace VSOSH.Dal.Services;

public class GeneralReportService : IGeneralReportService
{
    private readonly ILogger<GeneralReportService> _logger;
    private readonly IResultRepository _resultRepository;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GeneralReportService" />.
    /// </summary>
    /// <param name="resultRepository"><see cref="IResultRepository" />.</param>
    /// <param name="logger"><see cref="ILogger{GeneralReportService}" />.</param>
    /// <exception cref="ArgumentNullException">Если хотя бы один из аргументов не задан.</exception>
    public GeneralReportService(IResultRepository resultRepository, ILogger<GeneralReportService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FileStream> GetGeneralReport(CancellationToken cancellationToken = default)
    {
        var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, $"Общий_отчет.xlsx");
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        var generalReport = await _resultRepository.GetGeneralReport(cancellationToken);
        if (generalReport is null)
        {
            _logger.LogError("Нет данных для отчета.");
            throw new NotFoundException("Нет данных для отчета.");
        }

        var workbook = new XLWorkbook();
        workbook.Worksheets.Add("Отчет");
        var worksheet = workbook.Worksheets.Worksheet("Отчет");
        var row = worksheet.Row(1);
        row.Cell("A").Value = "Кол-во уникальных участников";
        row.Cell("B").Value = "Кол-во фактов участия";
        row.Cell("C").Value = "Кол-во уникальных победителей";
        row.Cell("D").Value = "Кол-во дипломов победителей";
        row.Cell("E").Value = "Кол-во уникальных призёров";
        row.Cell("F").Value = "Кол-во дипломов в призёров";
        row.Cell("G").Value = "Кол-во уникальных победителей и призёров";
        row = worksheet.Row(2);
        row.Cell("A").Value = generalReport.UniqueParticipants;
        row.Cell("B").Value = generalReport.TotalCount;
        row.Cell("C").Value = generalReport.UniqueWinners;
        row.Cell("D").Value = generalReport.TotalWinnerDiplomas;
        row.Cell("E").Value = generalReport.UniquePrizeWinners;
        row.Cell("F").Value = generalReport.TotalPrizeDiplomas;
        row.Cell("G").Value = generalReport.UniqueWinnersAndPrizeWinners;
        var stream = new FileStream(pathToFile, FileMode.OpenOrCreate);
        workbook.SaveAs(stream);
        return stream;
    }
}