using OfficeOpenXml;
using OfficeOpenXml.Style;
using VSOSH.Contracts;
using VSOSH.Domain;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using Subject = VSOSH.Domain.Subject;

namespace VSOSH.Dal.Services;

/// <summary>
/// Представляет реализацию <see cref="IQuantitativeDataService" />.
/// </summary>
public class QuantitativeDataService : IQuantitativeDataService
{
	#region Data
	#region Fields
	private IReadOnlyCollection<ArtResult>? _artResults;
	private IReadOnlyCollection<AstronomyResult>? _astronomyResults;
	private IReadOnlyCollection<BiologyResult>? _biologyResults;
	private IReadOnlyCollection<ChemistryResult>? _chemistryResults;
	private IReadOnlyCollection<ChineseResult>? _chineseResults;
	private IReadOnlyCollection<SpanishResult>? _spanishResults;
	private IReadOnlyCollection<ComputerScienceResult>? _computerScienceResults;
	private IReadOnlyCollection<EcologyResult>? _ecologyResults;
	private IReadOnlyCollection<EconomyResult>? _economyResults;
	private IReadOnlyCollection<EnglishResult>? _englishResults;
	private IReadOnlyCollection<FrenchResult>? _frenchResults;
	private IReadOnlyCollection<FundamentalsLifeSafetyResult>? _fundamentalsLifeSafetyResults;
	private IReadOnlyCollection<GeographyResult>? _geographyResults;
	private IReadOnlyCollection<GermanResult>? _germanResults;
	private IReadOnlyCollection<HistoryResult>? _historyResults;
	private IReadOnlyCollection<LawResult>? _lawResults;
	private IReadOnlyCollection<LiteratureResult>? _literatureResults;
	private IReadOnlyCollection<MathResult>? _mathResults;
	private IReadOnlyCollection<PhysicalEducationResult>? _physicalEducationResults;
	private IReadOnlyCollection<PhysicResult>? _physicResults;
	private readonly IResultRepository _resultRepository;
	private IReadOnlyCollection<RussianResult>? _russianResults;
	private IReadOnlyCollection<SocialStudiesResult>? _socialStudiesResults;
	private IReadOnlyCollection<TechnologyResult>? _technologyResults;
	private IReadOnlyCollection<ItalianResult> ? _italianResults;
	#endregion
	#endregion

	#region .ctor
	public QuantitativeDataService(IResultRepository resultRepository)
	{
		_resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));

		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
	}
	#endregion

	#region IQuantitativeDataService members
	public async Task<FileStream> GetQuantitativeData(CancellationToken cancellationToken = default)
	{
		var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, "Количественные_данные.xlsx");

		await InitialData(cancellationToken);

		using var excelPackage = new ExcelPackage();
		SetValueInExcel(excelPackage, Status.Participant, "Количество участников");
		SetValueInExcel(excelPackage, Status.Winner, "Количество победителей");
		SetValueInExcel(excelPackage, Status.Awardee, "Количество призеров");

		await excelPackage.SaveAsAsync(new FileInfo(pathToFile), cancellationToken);

		return new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
	}
	#endregion

	#region Private
	private static void CreateHeader(ExcelWorksheet worksheet)
	{
		worksheet.Cells[1, 1].Value = "Предмет";
		worksheet.Cells[1, 2].Value = "4";
		worksheet.Cells[1, 3].Value = "5";
		worksheet.Cells[1, 4].Value = "6";
		worksheet.Cells[1, 5].Value = "7";
		worksheet.Cells[1, 6].Value = "8";
		worksheet.Cells[1, 7].Value = "9";
		worksheet.Cells[1, 8].Value = "10";
		worksheet.Cells[1, 9].Value = "11";

		// Форматирование заголовка
		using var range = worksheet.Cells[1, 1, 1, 9];
		range.Style.Font.Bold = true;
		range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
	}

	private async Task InitialData(CancellationToken cancellationToken)
	{
		_artResults = await _resultRepository.FindRangeAsync<ArtResult>(cancellationToken: cancellationToken);
		_astronomyResults = await _resultRepository.FindRangeAsync<AstronomyResult>(cancellationToken: cancellationToken);
		_biologyResults = await _resultRepository.FindRangeAsync<BiologyResult>(cancellationToken: cancellationToken);
		_chemistryResults = await _resultRepository.FindRangeAsync<ChemistryResult>(cancellationToken: cancellationToken);
		_chineseResults = await _resultRepository.FindRangeAsync<ChineseResult>(cancellationToken: cancellationToken);
		_computerScienceResults = await _resultRepository.FindRangeAsync<ComputerScienceResult>(cancellationToken: cancellationToken);
		_ecologyResults = await _resultRepository.FindRangeAsync<EcologyResult>(cancellationToken: cancellationToken);
		_economyResults = await _resultRepository.FindRangeAsync<EconomyResult>(cancellationToken: cancellationToken);
		_englishResults = await _resultRepository.FindRangeAsync<EnglishResult>(cancellationToken: cancellationToken);
		_frenchResults = await _resultRepository.FindRangeAsync<FrenchResult>(cancellationToken: cancellationToken);
		_fundamentalsLifeSafetyResults = await _resultRepository.FindRangeAsync<FundamentalsLifeSafetyResult>(cancellationToken: cancellationToken);
		_geographyResults = await _resultRepository.FindRangeAsync<GeographyResult>(cancellationToken: cancellationToken);
		_germanResults = await _resultRepository.FindRangeAsync<GermanResult>(cancellationToken: cancellationToken);
		_historyResults = await _resultRepository.FindRangeAsync<HistoryResult>(cancellationToken: cancellationToken);
		_lawResults = await _resultRepository.FindRangeAsync<LawResult>(cancellationToken: cancellationToken);
		_literatureResults = await _resultRepository.FindRangeAsync<LiteratureResult>(cancellationToken: cancellationToken);
		_mathResults = await _resultRepository.FindRangeAsync<MathResult>(cancellationToken: cancellationToken);
		_physicalEducationResults = await _resultRepository.FindRangeAsync<PhysicalEducationResult>(cancellationToken: cancellationToken);
		_physicResults = await _resultRepository.FindRangeAsync<PhysicResult>(cancellationToken: cancellationToken);
		_russianResults = await _resultRepository.FindRangeAsync<RussianResult>(cancellationToken: cancellationToken);
		_socialStudiesResults = await _resultRepository.FindRangeAsync<SocialStudiesResult>(cancellationToken: cancellationToken);
		_technologyResults = await _resultRepository.FindRangeAsync<TechnologyResult>(cancellationToken: cancellationToken);
		_spanishResults = await _resultRepository.FindRangeAsync<SpanishResult>(cancellationToken: cancellationToken);
		_italianResults = await _resultRepository.FindRangeAsync<ItalianResult>(cancellationToken: cancellationToken);
	}

	private void SetValueInExcel(ExcelPackage excelPackage, Status status, string sheetName)
	{
		var worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
		CreateHeader(worksheet);

		SetValues(worksheet, 2, _artResults.Where(r => r.Status == status), _artResults?.Count > 0 ? null : Subject.Art);
		SetValues(worksheet, 3, _astronomyResults.Where(r => r.Status == status), _astronomyResults?.Count > 0 ? null : Subject.Astronomy);
		SetValues(worksheet, 3, _biologyResults.Where(r => r.Status == status), _astronomyResults?.Count > 0 ? null : Subject.Astronomy);
		SetValues(worksheet, 4, _biologyResults?.Where(r => r.Status == status), _biologyResults?.Count > 0 ? null : Subject.Biology);
		SetValues(worksheet, 5, _chemistryResults?.Where(r => r.Status == status), _chemistryResults?.Count > 0 ? null : Subject.Chemistry);
		SetValues(worksheet, 6, _chineseResults?.Where(r => r.Status == status), _chineseResults?.Count > 0 ? null : Subject.Chinese);
		SetValues(worksheet, 7, _computerScienceResults?.Where(r => r.Status == status), _computerScienceResults?.Count > 0 ? null : Subject.ComputerScience);
		SetValues(worksheet, 8, _ecologyResults?.Where(r => r.Status == status), _ecologyResults?.Count > 0 ? null : Subject.Ecology);
		SetValues(worksheet, 9, _economyResults?.Where(r => r.Status == status), _economyResults?.Count > 0 ? null : Subject.Economy);
		SetValues(worksheet, 10, _englishResults?.Where(r => r.Status == status), _englishResults?.Count > 0 ? null : Subject.English);
		SetValues(worksheet, 11, _frenchResults?.Where(r => r.Status == status), _frenchResults?.Count > 0 ? null : Subject.French);
		SetValues(worksheet, 12, _fundamentalsLifeSafetyResults?.Where(r => r.Status == status), _fundamentalsLifeSafetyResults?.Count > 0 ? null : Subject.FundamentalsLifeSafety);
		SetValues(worksheet, 13, _geographyResults?.Where(r => r.Status == status), _geographyResults?.Count > 0 ? null : Subject.Geography);
		SetValues(worksheet, 14, _germanResults?.Where(r => r.Status == status), _germanResults?.Count > 0 ? null : Subject.German);
		SetValues(worksheet, 15, _historyResults?.Where(r => r.Status == status), _historyResults?.Count > 0 ? null : Subject.History);
		SetValues(worksheet, 16, _lawResults?.Where(r => r.Status == status), _lawResults?.Count > 0 ? null : Subject.Law);
		SetValues(worksheet, 17, _literatureResults?.Where(r => r.Status == status), _literatureResults?.Count > 0 ? null : Subject.Literature);
		SetValues(worksheet, 18, _mathResults?.Where(r => r.Status == status), _mathResults?.Count > 0 ? null : Subject.Math);
		SetValues(worksheet, 19, _physicalEducationResults?.Where(r => r.Status == status), _physicalEducationResults?.Count > 0 ? null : Subject.PhysicalEducation);
		SetValues(worksheet, 20, _physicResults?.Where(r => r.Status == status), _physicResults?.Count > 0 ? null : Subject.Physic);
		SetValues(worksheet, 21, _russianResults?.Where(r => r.Status == status), _russianResults?.Count > 0 ? null : Subject.Russian);
		SetValues(worksheet, 22, _socialStudiesResults?.Where(r => r.Status == status), _socialStudiesResults?.Count > 0 ? null : Subject.SocialStudies);
		SetValues(worksheet, 23, _technologyResults?.Where(r => r.Status == status), _technologyResults?.Count > 0 ? null : Subject.Technology);
		SetValues(worksheet, 24, _spanishResults?.Where(r => r.Status == status), _spanishResults?.Count > 0 ? null : Subject.Spanish);
		SetValues(worksheet, 25, _italianResults?.Where(r => r.Status == status), _italianResults?.Count > 0 ? null : Subject.Italian);
		worksheet.Cells[worksheet.Dimension.Address]
				 .AutoFitColumns();
	}

	private static void SetValues<T>(ExcelWorksheet worksheet, int row, IEnumerable<T> results, Subject? subject) where T : SchoolOlympiadResultBase
	{
		var subjectName = results.FirstOrDefault()
								 ?.GetResultName();
		if (subject is not null && subjectName is null)
		{
			subjectName = subject.Value.GetString();
		}

		worksheet.Cells[row, 1].Value = subjectName;
		worksheet.Cells[row, 2].Value = results.Count(r => r.GradeCompeting == 4);
		worksheet.Cells[row, 3].Value = results.Count(r => r.GradeCompeting == 5);
		worksheet.Cells[row, 4].Value = results.Count(r => r.GradeCompeting == 6);
		worksheet.Cells[row, 5].Value = results.Count(r => r.GradeCompeting == 7);
		worksheet.Cells[row, 6].Value = results.Count(r => r.GradeCompeting == 8);
		worksheet.Cells[row, 7].Value = results.Count(r => r.GradeCompeting == 9);
		worksheet.Cells[row, 8].Value = results.Count(r => r.GradeCompeting == 10);
		worksheet.Cells[row, 9].Value = results.Count(r => r.GradeCompeting == 11);
	}
	#endregion
}
