using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using VSOSH.Contracts;
using VSOSH.Contracts.Exceptions;
using VSOSH.Domain;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using Subject = VSOSH.Domain.Subject;

namespace VSOSH.Dal.Services;

/// <summary>
/// Представляет реализцаию <see cref="IPassingPointsService" />.
/// </summary>
public class PassingPointsService : IPassingPointsService
{
    private readonly ILogger<PassingPointsService> _logger;
    private readonly IResultRepository _resultRepository;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PassingPointsService" />.
    /// </summary>
    /// <param name="resultRepository"><see cref="IResultRepository" />.</param>
    /// <param name="logger"><see cref="ILogger{PassingPointsService}" />.</param>
    /// <exception cref="ArgumentNullException">Если хотя бы один из аргументов не задан.</exception>
    public PassingPointsService(IResultRepository resultRepository, ILogger<PassingPointsService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<FileStream> GetPassingPoints(Subject subject, CancellationToken cancellationToken = default)
    {
        var pathToFile =
            Path.Combine(ProfileLocationStorage.ServiceFiles, $"Проходной_балл_{subject.GetString()}.xlsx");
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        var results = await GetResults(subject, cancellationToken);
        var workbook = new XLWorkbook();
        if (results is null || results.Count == 0)
        {
            const string message = "Нет данных для формирования проходных баллов.";
            _logger.LogError(message);
            throw new NotFoundException(message);
        }

        int? previousGradeCompeting = null;
        IXLWorksheet? workSheet = null;
        var row = 2;
        if (subject == Subject.PhysicalEducation)
        {
            var males = results.Select(s => s as PhysicalEducationResult)
                .Where(s => s.Sex == Sex.Male);
            var females = results.Select(s => s as PhysicalEducationResult)
                .Where(s => s.Sex == Sex.Female);
            foreach (var result in males)
            {
                if (!previousGradeCompeting.HasValue || previousGradeCompeting != result.GradeCompeting)
                {
                    var isExists = workbook.Worksheets.TryGetWorksheet($"{result.GradeCompeting.ToString()}М",
                        out workSheet);
                    if (isExists) continue;
                    workbook.Worksheets.Add($"{result.GradeCompeting.ToString()}М");
                    workSheet = workbook.Worksheets.Worksheet($"{result.GradeCompeting.ToString()}М");
                    previousGradeCompeting = result.GradeCompeting;
                    CreateHeader(workSheet.Row(1), subject);
                    row = 2;
                }

                AddDataInWorkSheet(workSheet!.Row(row++), result, subject);
            }

            row = 2;
            foreach (var result in females)
            {
                if (!previousGradeCompeting.HasValue || previousGradeCompeting != result.GradeCompeting)
                {
                    var isExists = workbook.Worksheets.TryGetWorksheet($"{result.GradeCompeting.ToString()}Ж",
                        out workSheet);
                    if (isExists) continue;
                    workbook.Worksheets.Add($"{result.GradeCompeting.ToString()}Ж");
                    workSheet = workbook.Worksheets.Worksheet($"{result.GradeCompeting.ToString()}Ж");
                    previousGradeCompeting = result.GradeCompeting;
                    CreateHeader(workSheet.Row(1), subject);
                    row = 2;
                }

                AddDataInWorkSheet(workSheet!.Row(row++), result, subject);
            }
        }
        else
        {
            foreach (var result in results)
            {
                if (!previousGradeCompeting.HasValue || previousGradeCompeting != result.GradeCompeting)
                {
                    var isExists = workbook.Worksheets.TryGetWorksheet(result.GradeCompeting.ToString(),
                        out workSheet);
                    if (isExists) continue;
                    workbook.Worksheets.Add(result.GradeCompeting.ToString());
                    workSheet = workbook.Worksheets.Worksheet(result.GradeCompeting.ToString());
                    previousGradeCompeting = result.GradeCompeting;
                    CreateHeader(workSheet.Row(1), subject);
                    row = 2;
                }

                AddDataInWorkSheet(workSheet!.Row(row++), result, subject);
            }
        }

        foreach (var worksheet in workbook.Worksheets)
        {
            Sort(worksheet, subject);
        }

        var stream = new FileStream(pathToFile, FileMode.OpenOrCreate);
        workbook.SaveAs(stream);
        return stream;
    }

    private void CreateHeader(IXLRow row, Subject subject)
    {
        if (subject is not (Subject.PhysicalEducation or Subject.Technology))
        {
            CreateHeaderBase(row);
        }
        else if (subject == Subject.PhysicalEducation)
        {
            CreateHeaderPe(row);
        }
        else
        {
            CreateHeaderTech(row);
        }
    }

    private static void Sort(IXLWorksheet? workSheet, Subject subject)
    {
        if (subject is Subject.PhysicalEducation or Subject.Technology)
        {
            workSheet?.Sort(ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe, XLSortOrder.Descending);
        }

        workSheet?.Sort(ExcelPassingPointsInfo.ColumnWithFinalScore.Base, XLSortOrder.Descending);
    }

    private static void CreateHeaderTech(IXLRow row)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue("ОУ");
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue("Фамилия");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue("Имя");
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base).SetValue("Класс, за который выступает");
        row.Cell(ExcelPassingPointsInfo.ColumnWithPractice).SetValue("Направление практики");
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe).SetValue("%");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe).SetValue("Итоговый балл");
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe).SetValue("Статус");
    }

    private static void CreateHeaderPe(IXLRow row)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue("ОУ");
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue("Фамилия");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue("Имя");
        row.Cell(ExcelPassingPointsInfo.ColumnWithSex).SetValue("Пол");
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Pe).SetValue("Класс, за который выступает");
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe).SetValue("%");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe).SetValue("Итоговый балл");
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe).SetValue("Статус");
    }

    private static void AddDataInWorkSheet(IXLRow row, SchoolOlympiadResultBase result, Subject subject)
    {
        switch (subject)
        {
            case Subject.PhysicalEducation:
                AddDataInWorkSheetPe(row, result as PhysicalEducationResult);
                break;
            case Subject.Technology:
                AddDataInWorkSheetTechnology(row, result as TechnologyResult);
                break;
            default:
                AddDataInWorkSheetBase(row, result);
                break;
        }
    }

    private static void AddDataInWorkSheetPe(IXLRow row, PhysicalEducationResult result)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue(result.School);
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue(result.StudentName.LastName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue(result.StudentName.FirstName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithSex).SetValue(result.Sex.GetString());
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Pe).SetValue(result.GradeCompeting);
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe).SetValue(result.Percentage);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe).SetValue(result.FinalScore);
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe).SetValue(result.Status.GetString());
    }

    private static void AddDataInWorkSheetTechnology(IXLRow row, TechnologyResult result)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue(result.School);
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue(result.StudentName.LastName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue(result.StudentName.FirstName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base).SetValue(result.GradeCompeting);
        ;
        row.Cell(ExcelPassingPointsInfo.ColumnWithPractice).SetValue(result.DirectionPractice);
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe).SetValue(result.Percentage);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe).SetValue(result.FinalScore);
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe).SetValue(result.Status.GetString());
    }

    private static void AddDataInWorkSheetBase(IXLRow row, SchoolOlympiadResultBase result)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue(result.School);
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue(result.StudentName.LastName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue(result.StudentName.FirstName);
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base).SetValue(result.GradeCompeting);
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.Base).SetValue(result.Percentage);
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.Base).SetValue(result.FinalScore);
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.Base).SetValue(result.Status.GetString());
    }

    private static void CreateHeaderBase(IXLRow row)
    {
        row.Cell(ExcelPassingPointsInfo.ColumnWithSchoolName).SetValue("ОУ");
        row.Cell(ExcelPassingPointsInfo.ColumnWithLastName).SetValue("Фамилия");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFirstName).SetValue("Имя");
        row.Cell(ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base).SetValue("Класс, за который выступает");
        row.Cell(ExcelPassingPointsInfo.ColumnWithPercentage.Base).SetValue("%");
        row.Cell(ExcelPassingPointsInfo.ColumnWithFinalScore.Base).SetValue("Итоговый балл");
        row.Cell(ExcelPassingPointsInfo.ColumnWithStatus.Base).SetValue("Статус");
    }

    private async Task<IReadOnlyCollection<SchoolOlympiadResultBase>?> GetResults(Subject subject,
        CancellationToken cancellationToken)
    {
        return subject switch
        {
            Subject.Art => await _resultRepository.FindRangeAsync<ArtResult>(cancellationToken: cancellationToken),
            Subject.Astronomy => await _resultRepository.FindRangeAsync<AstronomyResult>(
                cancellationToken: cancellationToken),
            Subject.Biology => await _resultRepository.FindRangeAsync<BiologyResult>(
                cancellationToken: cancellationToken),
            Subject.Chemistry => await _resultRepository.FindRangeAsync<ChemistryResult>(
                cancellationToken: cancellationToken),
            Subject.Chinese => await _resultRepository.FindRangeAsync<ChineseResult>(
                cancellationToken: cancellationToken),
            Subject.ComputerScience => await _resultRepository.FindRangeAsync<ComputerScienceResult>(
                cancellationToken: cancellationToken),
            Subject.Ecology => await _resultRepository.FindRangeAsync<EcologyResult>(
                cancellationToken: cancellationToken),
            Subject.Economy => await _resultRepository.FindRangeAsync<EconomyResult>(
                cancellationToken: cancellationToken),
            Subject.English => await _resultRepository.FindRangeAsync<EnglishResult>(
                cancellationToken: cancellationToken),
            Subject.French =>
                await _resultRepository.FindRangeAsync<FrenchResult>(cancellationToken: cancellationToken),
            Subject.FundamentalsLifeSafety => await _resultRepository.FindRangeAsync<FundamentalsLifeSafetyResult>(
                cancellationToken: cancellationToken),
            Subject.Geography => await _resultRepository.FindRangeAsync<GeographyResult>(
                cancellationToken: cancellationToken),
            Subject.German =>
                await _resultRepository.FindRangeAsync<GermanResult>(cancellationToken: cancellationToken),
            Subject.History => await _resultRepository.FindRangeAsync<HistoryResult>(
                cancellationToken: cancellationToken),
            Subject.Law => await _resultRepository.FindRangeAsync<LawResult>(cancellationToken: cancellationToken),
            Subject.Literature => await _resultRepository.FindRangeAsync<LiteratureResult>(
                cancellationToken: cancellationToken),
            Subject.Math => await _resultRepository.FindRangeAsync<MathResult>(cancellationToken: cancellationToken),
            Subject.PhysicalEducation => await _resultRepository.FindRangeAsync<PhysicalEducationResult>(
                cancellationToken: cancellationToken),
            Subject.Physic =>
                await _resultRepository.FindRangeAsync<PhysicResult>(cancellationToken: cancellationToken),
            Subject.Russian => await _resultRepository.FindRangeAsync<RussianResult>(
                cancellationToken: cancellationToken),
            Subject.SocialStudies => await _resultRepository.FindRangeAsync<SocialStudiesResult>(
                cancellationToken: cancellationToken),
            Subject.Technology => await _resultRepository.FindRangeAsync<TechnologyResult>(
                cancellationToken: cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(subject), subject, null)
        };
    }
}