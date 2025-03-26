using OfficeOpenXml;
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

    public GreaterClassService(IResultRepository resultRepository, ILogger<GreaterClassService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        // Установка лицензионного контекста
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // или Commercial, если есть лицензия
    }

    public async Task<FileStream> GetGreaterClass(CancellationToken cancellationToken = default)
    {
        var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, $"За_более_старший_класс.xlsx");
        
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        var results = await _resultRepository.FindRangeAsync<SchoolOlympiadResultBase>(
            r => r.CurrentCompeting.HasValue, cancellationToken);

        using (var excelPackage = new ExcelPackage())
        {
            var worksheet = excelPackage.Workbook.Worksheets.Add("1");
            
            // Создание заголовков
            CreateHeader(worksheet);
            
            // Заполнение данных
            var row = 2;
            foreach (var result in results)
            {
                worksheet.Cells[row, 1].Value = result.GetResultName();
                worksheet.Cells[row, 2].Value = result.StudentName.LastName;
                worksheet.Cells[row, 3].Value = result.StudentName.FirstName;
                worksheet.Cells[row, 4].Value = result.StudentName.MiddleName ?? string.Empty;
                worksheet.Cells[row, 5].Value = result.GradeCompeting;
                worksheet.Cells[row, 6].Value = result.CurrentCompeting;
                row++;
            }

            // Сохранение файла
            var fileInfo = new FileInfo(pathToFile);
            await excelPackage.SaveAsAsync(fileInfo, cancellationToken);
        }

        return new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
    }

    private static void CreateHeader(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 1].Value = "Предмет";
        worksheet.Cells[1, 2].Value = "Фамилия";
        worksheet.Cells[1, 3].Value = "Имя";
        worksheet.Cells[1, 4].Value = "Отчество";
        worksheet.Cells[1, 5].Value = "Класс, за который выступает";
        worksheet.Cells[1, 6].Value = "Класс, в котором учится";
        
        // Опционально: стилизация заголовков
        using var range = worksheet.Cells[1, 1, 1, 6];
        range.Style.Font.Bold = true;
        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
    }
}