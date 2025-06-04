using OfficeOpenXml;
using Serilog;
using VSOSH.Contracts;
using VSOSH.Contracts.Exceptions;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;

namespace VSOSH.Dal.Services;

public class GeneralReportService : IGeneralReportService
{
	#region Data
	#region Static
	private static readonly ILogger Log = Serilog.Log.ForContext<PassingPointsService>();
	#endregion

	#region Fields
	private readonly IResultRepository _resultRepository;
	#endregion
	#endregion

	#region .ctor
	public GeneralReportService(IResultRepository resultRepository)
	{
		_resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
	}
	#endregion

	#region IGeneralReportService members
	public async Task<FileStream> GetGeneralReport(CancellationToken cancellationToken = default)
	{
		var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, $"Общий_отчет.xlsx");

		var generalReport = await _resultRepository.GetGeneralReport(cancellationToken);
		if (generalReport is null)
		{
			Log.Error("Нет данных для отчета.");
			throw new NotFoundException("Нет данных для отчета.");
		}

		// Создаем Excel-файл с помощью EPPlus
		using (var excelPackage = new ExcelPackage())
		{
			var worksheet = excelPackage.Workbook.Worksheets.Add("Отчет");

			// Заголовки
			worksheet.Cells["A1"].Value = "Кол-во уникальных участников";
			worksheet.Cells["B1"].Value = "Кол-во фактов участия";
			worksheet.Cells["C1"].Value = "Кол-во уникальных победителей";
			worksheet.Cells["D1"].Value = "Кол-во дипломов победителей";
			worksheet.Cells["E1"].Value = "Кол-во уникальных призёров";
			worksheet.Cells["F1"].Value = "Кол-во дипломов в призёров";
			worksheet.Cells["G1"].Value = "Кол-во уникальных победителей и призёров";

			// Данные
			worksheet.Cells["A2"].Value = generalReport.UniqueParticipants;
			worksheet.Cells["B2"].Value = generalReport.TotalCount;
			worksheet.Cells["C2"].Value = generalReport.UniqueWinners;
			worksheet.Cells["D2"].Value = generalReport.TotalWinnerDiplomas;
			worksheet.Cells["E2"].Value = generalReport.UniquePrizeWinners;
			worksheet.Cells["F2"].Value = generalReport.TotalPrizeDiplomas;
			worksheet.Cells["G2"].Value = generalReport.UniqueWinnersAndPrizeWinners;

			// Сохраняем в файл
			var fileInfo = new FileInfo(pathToFile);
			await excelPackage.SaveAsAsync(fileInfo, cancellationToken);
		}

		// Возвращаем поток с файлом
		return new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
	}
	#endregion
}
