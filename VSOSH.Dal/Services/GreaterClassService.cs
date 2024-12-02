using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using VSOSH.Contracts;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;

namespace VSOSH.Dal.Services;

public class GreaterClassService : IGreaterClassService
{
    private readonly ILogger<GreaterClassService> _logger;
    private readonly IResultRepository _resultRepository;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GreaterClassService" />.
    /// </summary>
    /// <param name="resultRepository"><see cref="IResultRepository" />.</param>
    /// <param name="logger"><see cref="ILogger{GreaterClassService}" />.</param>
    /// <exception cref="ArgumentNullException">Если хотя бы один из аргументов не задан.</exception>
    public GreaterClassService(IResultRepository resultRepository, ILogger<GreaterClassService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FileStream> GetGreaterClass(CancellationToken cancellationToken = default)
    {
        var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, $"За_более_старший_класс.xlsx");
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        var results =
            await _resultRepository.FindRangeAsync<SchoolOlympiadResultBase>(r => r.CurrentCompeting.HasValue,
                cancellationToken);

        var workbook = new XLWorkbook();
        workbook.Worksheets.Add("1");
        var worksheet = workbook.Worksheets.Worksheet("1");
        CreateHeader(worksheet.Row(1));
        var row = 2;
        foreach (var result in results)
        {
            var xlRow = worksheet.Row(row++);
            xlRow.Cell("A").SetValue(result.GetResultName());
            xlRow.Cell("B").SetValue(result.StudentName.LastName);
            xlRow.Cell("C").SetValue(result.StudentName.FirstName);
            xlRow.Cell("D").SetValue(result.StudentName.MiddleName);
            xlRow.Cell("E").SetValue(result.GradeCompeting);
            xlRow.Cell("F").SetValue(result.CurrentCompeting);
        }

        var stream = new FileStream(pathToFile, FileMode.OpenOrCreate);
        workbook.SaveAs(stream);
        return stream;
    }

    private static void CreateHeader(IXLRow row)
    {
        row.Cell("A").SetValue("Предмет");
        row.Cell("B").SetValue("Фамилия");
        row.Cell("C").SetValue("Имя");
        row.Cell("D").SetValue("Отчество");
        row.Cell("E").SetValue("Класс, за который выступает");
        row.Cell("F").SetValue("Класс, в котором учится");
    }
}