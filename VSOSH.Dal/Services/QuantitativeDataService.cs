using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<QuantitativeDataService> _logger;
    private readonly IResultRepository _resultRepository;
    private IReadOnlyCollection<ArtResult>? _artResults;
    private IReadOnlyCollection<AstronomyResult>? _astronomyResults;
    private IReadOnlyCollection<BiologyResult>? _biologyResults;
    private IReadOnlyCollection<ChemistryResult>? _chemistryResults;
    private IReadOnlyCollection<ChineseResult>? _chineseResults;
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
    private IReadOnlyCollection<RussianResult>? _russianResults;
    private IReadOnlyCollection<SocialStudiesResult>? _socialStudiesResults;
    private IReadOnlyCollection<TechnologyResult>? _technologyResults;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="QuantitativeDataService" />.
    /// </summary>
    /// <param name="resultRepository"><see cref="IResultRepository" />.</param>
    /// <param name="logger"><see cref="ILogger{QuantitativeDataService}" />.</param>
    /// <exception cref="ArgumentNullException">Если хотя бы один из аргументов не задан.</exception>
    public QuantitativeDataService(IResultRepository resultRepository, ILogger<QuantitativeDataService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<FileStream> GetQuantitativeData(CancellationToken cancellationToken = default)
    {
        var pathToFile = Path.Combine(ProfileLocationStorage.ServiceFiles, $"Количественные_данные.xlsx");
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        await InitialData(cancellationToken);
        var workbook = new XLWorkbook();
        SetValueInExcel(workbook, Status.Participant, "Количество участников");
        SetValueInExcel(workbook, Status.Winner, "Количество победителей");
        SetValueInExcel(workbook, Status.Awardee, "Количество призеров");
        var stream = new FileStream(pathToFile, FileMode.OpenOrCreate);
        workbook.SaveAs(stream);
        return stream;
    }

    private async Task InitialData(CancellationToken cancellationToken)
    {
        _artResults = await _resultRepository.FindRangeAsync<ArtResult>(cancellationToken: cancellationToken);
        _astronomyResults =
            await _resultRepository.FindRangeAsync<AstronomyResult>(cancellationToken: cancellationToken);
        _biologyResults = await _resultRepository.FindRangeAsync<BiologyResult>(cancellationToken: cancellationToken);
        _chemistryResults =
            await _resultRepository.FindRangeAsync<ChemistryResult>(cancellationToken: cancellationToken);
        _chineseResults = await _resultRepository.FindRangeAsync<ChineseResult>(cancellationToken: cancellationToken);
        _computerScienceResults =
            await _resultRepository.FindRangeAsync<ComputerScienceResult>(cancellationToken: cancellationToken);
        _ecologyResults = await _resultRepository.FindRangeAsync<EcologyResult>(cancellationToken: cancellationToken);
        _economyResults = await _resultRepository.FindRangeAsync<EconomyResult>(cancellationToken: cancellationToken);
        _englishResults = await _resultRepository.FindRangeAsync<EnglishResult>(cancellationToken: cancellationToken);
        _frenchResults = await _resultRepository.FindRangeAsync<FrenchResult>(cancellationToken: cancellationToken);
        _fundamentalsLifeSafetyResults =
            await _resultRepository.FindRangeAsync<FundamentalsLifeSafetyResult>(cancellationToken: cancellationToken);
        _geographyResults =
            await _resultRepository.FindRangeAsync<GeographyResult>(cancellationToken: cancellationToken);
        _germanResults = await _resultRepository.FindRangeAsync<GermanResult>(cancellationToken: cancellationToken);
        _historyResults = await _resultRepository.FindRangeAsync<HistoryResult>(cancellationToken: cancellationToken);
        _lawResults = await _resultRepository.FindRangeAsync<LawResult>(cancellationToken: cancellationToken);
        _literatureResults =
            await _resultRepository.FindRangeAsync<LiteratureResult>(cancellationToken: cancellationToken);
        _mathResults = await _resultRepository.FindRangeAsync<MathResult>(cancellationToken: cancellationToken);
        _physicalEducationResults =
            await _resultRepository.FindRangeAsync<PhysicalEducationResult>(cancellationToken: cancellationToken);
        _physicResults = await _resultRepository.FindRangeAsync<PhysicResult>(cancellationToken: cancellationToken);
        _russianResults = await _resultRepository.FindRangeAsync<RussianResult>(cancellationToken: cancellationToken);
        _socialStudiesResults =
            await _resultRepository.FindRangeAsync<SocialStudiesResult>(cancellationToken: cancellationToken);
        _technologyResults =
            await _resultRepository.FindRangeAsync<TechnologyResult>(cancellationToken: cancellationToken);
    }

    private void SetValueInExcel(IXLWorkbook workbook, Status status, string sheetName)
    {
        workbook.Worksheets.Add(sheetName);
        var worksheet = workbook.Worksheets.Worksheet(sheetName);
        CreateHeader(worksheet.Row(1));
        SetValues(worksheet.Row(2), _artResults.Where(r => r.Status == status),
            _artResults?.Count > 0 ? null : Subject.Art);
        SetValues(worksheet.Row(3), _astronomyResults.Where(r => r.Status == status),
            _astronomyResults?.Count > 0 ? null : Subject.Astronomy);
        SetValues(worksheet.Row(4), _biologyResults.Where(r => r.Status == status),
            _biologyResults?.Count > 0 ? null : Subject.Biology);
        SetValues(worksheet.Row(5), _chemistryResults.Where(r => r.Status == status),
            _chemistryResults?.Count > 0 ? null : Subject.Chemistry);
        SetValues(worksheet.Row(6), _chineseResults.Where(r => r.Status == status),
            _chineseResults?.Count > 0 ? null : Subject.Chinese);
        SetValues(worksheet.Row(7), _computerScienceResults.Where(r => r.Status == status),
            _computerScienceResults?.Count > 0 ? null : Subject.ComputerScience);
        SetValues(worksheet.Row(8), _ecologyResults.Where(r => r.Status == status),
            _ecologyResults?.Count > 0 ? null : Subject.Ecology);
        SetValues(worksheet.Row(9), _economyResults.Where(r => r.Status == status),
            _economyResults?.Count > 0 ? null : Subject.Economy);
        SetValues(worksheet.Row(10), _englishResults.Where(r => r.Status == status),
            _englishResults?.Count > 0 ? null : Subject.English);
        SetValues(worksheet.Row(11), _frenchResults.Where(r => r.Status == status),
            _frenchResults?.Count > 0 ? null : Subject.French);
        SetValues(worksheet.Row(12), _fundamentalsLifeSafetyResults.Where(r => r.Status == status),
            _fundamentalsLifeSafetyResults?.Count > 0 ? null : Subject.FundamentalsLifeSafety);
        SetValues(worksheet.Row(13), _geographyResults.Where(r => r.Status == status),
            _geographyResults?.Count > 0 ? null : Subject.Geography);
        SetValues(worksheet.Row(14), _germanResults.Where(r => r.Status == status),
            _germanResults?.Count > 0 ? null : Subject.German);
        SetValues(worksheet.Row(15), _historyResults.Where(r => r.Status == status),
            _historyResults?.Count > 0 ? null : Subject.History);
        SetValues(worksheet.Row(16), _lawResults.Where(r => r.Status == status),
            _lawResults?.Count > 0 ? null : Subject.Law);
        SetValues(worksheet.Row(17), _literatureResults.Where(r => r.Status == status),
            _literatureResults?.Count > 0 ? null : Subject.Literature);
        SetValues(worksheet.Row(18), _mathResults.Where(r => r.Status == status),
            _mathResults?.Count > 0 ? null : Subject.Math);
        SetValues(worksheet.Row(19), _physicalEducationResults.Where(r => r.Status == status),
            _physicalEducationResults?.Count > 0 ? null : Subject.PhysicalEducation);
        SetValues(worksheet.Row(20), _physicResults.Where(r => r.Status == status),
            _physicResults?.Count > 0 ? null : Subject.Physic);
        SetValues(worksheet.Row(21), _russianResults.Where(r => r.Status == status),
            _russianResults?.Count > 0 ? null : Subject.Russian);
        SetValues(worksheet.Row(22), _socialStudiesResults.Where(r => r.Status == status),
            _socialStudiesResults?.Count > 0 ? null : Subject.SocialStudies);
        SetValues(worksheet.Row(23), _technologyResults.Where(r => r.Status == status),
            _technologyResults?.Count > 0 ? null : Subject.Technology);
    }

    private static void CreateHeader(IXLRow row)
    {
        row.Cell("A").SetValue("Предмет");
        row.Cell("B").SetValue("4");
        row.Cell("C").SetValue("5");
        row.Cell("D").SetValue("6");
        row.Cell("E").SetValue("7");
        row.Cell("F").SetValue("8");
        row.Cell("G").SetValue("9");
        row.Cell("H").SetValue("10");
        row.Cell("I").SetValue("11");
    }

    private void SetDefault(IXLRow row, string subjectName)
    {
    }

    private void SetValues<T>(IXLRow row, IEnumerable<T> results, Subject? subject) where T : SchoolOlympiadResultBase
    {
        var subjectName = results.FirstOrDefault()?.GetResultName();
        if (subject is not null && subjectName is null)
        {
            subjectName = subject.Value.GetString();
        }

        row.Cell("A").SetValue(subjectName);
        row.Cell("B").SetValue(results.Count(r => r.GradeCompeting == 4));
        row.Cell("C").SetValue(results.Count(r => r.GradeCompeting == 5));
        row.Cell("D").SetValue(results.Count(r => r.GradeCompeting == 6));
        row.Cell("E").SetValue(results.Count(r => r.GradeCompeting == 7));
        row.Cell("F").SetValue(results.Count(r => r.GradeCompeting == 8));
        row.Cell("G").SetValue(results.Count(r => r.GradeCompeting == 9));
        row.Cell("H").SetValue(results.Count(r => r.GradeCompeting == 10));
        row.Cell("H").SetValue(results.Count(r => r.GradeCompeting == 11));
    }
}