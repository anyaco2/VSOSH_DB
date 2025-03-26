using OfficeOpenXml;
using Microsoft.Extensions.Logging;
using VSOSH.Contracts;
using VSOSH.Contracts.Exceptions;
using VSOSH.Domain;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using Subject = VSOSH.Domain.Subject;

namespace VSOSH.Dal.Services;

public class PassingPointsService : IPassingPointsService
{
    private readonly ILogger<PassingPointsService> _logger;
    private readonly IResultRepository _resultRepository;

    public PassingPointsService(IResultRepository resultRepository, ILogger<PassingPointsService> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<FileStream> GetPassingPoints(Subject subject, CancellationToken cancellationToken = default)
    {
        var pathToFile =
            Path.Combine(ProfileLocationStorage.ServiceFiles, $"Проходной_балл_{subject.GetString()}.xlsx");

        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }

        var results = await GetResults(subject, cancellationToken);

        using (var excelPackage = new ExcelPackage())
        {
            if (results is null || results.Count == 0)
            {
                const string message = "Нет данных для формирования проходных баллов.";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            ExcelWorksheet? workSheet = null;
            var row = 2;

            if (subject == Subject.PhysicalEducation)
            {
                var males = results
                            .SelectMany(g => g.Select(s => s as PhysicalEducationResult).Where(s => s.Sex == Sex.Male))
                            .GroupBy(s => s.GradeCompeting);
                var females = results
                            .SelectMany(
                                g => g.Select(s => s as PhysicalEducationResult).Where(s => s.Sex == Sex.Female))
                            .GroupBy(s => s.GradeCompeting);
                foreach (var result in males)
                {
                    workSheet = excelPackage.Workbook.Worksheets.Add($"{result.Key}М");
                    CreateHeader(workSheet, subject);
                    row = 2;
                    foreach (var resultBase in result.Where(s => s.GradeCompeting == result.Key).OrderByDescending(s => s.FinalScore))
                    {
                        AddDataInWorkSheet(workSheet, row++, resultBase, subject);
                    }
                }

                foreach (var result in females)
                {
                    workSheet = excelPackage.Workbook.Worksheets.Add($"{result.Key}Ж");
                    CreateHeader(workSheet, subject);
                    row = 2;
                    foreach (var resultBase in result.Where(s => s.GradeCompeting == result.Key).OrderByDescending(s => s.FinalScore))
                    {
                        AddDataInWorkSheet(workSheet, row++, resultBase, subject);
                    }
                }
            }
            else
            {
                foreach (var result in results)
                {
                    workSheet = excelPackage.Workbook.Worksheets.Add(result.Key.ToString());
                    CreateHeader(workSheet, subject);
                    row = 2;
                    foreach (var resultBase in result.Select(s => s).OrderByDescending(s => s.FinalScore))
                    {
                        AddDataInWorkSheet(workSheet, row++, resultBase, subject);
                    }
                }
            }
            excelPackage.SaveAs(new FileInfo(pathToFile));
        }

        return new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
    }

    private void CreateHeader(ExcelWorksheet worksheet, Subject subject)
    {
        if (subject is not (Subject.PhysicalEducation or Subject.Technology))
        {
            CreateHeaderBase(worksheet);
        }
        else if (subject == Subject.PhysicalEducation)
        {
            CreateHeaderPe(worksheet);
        }
        else
        {
            CreateHeaderTech(worksheet);
        }
    }

    private static void CreateHeaderTech(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = "ОУ";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithLastName].Value = "Фамилия";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFirstName].Value = "Имя";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base].Value = "Класс, за который выступает";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithPractice].Value = "Направление практики";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe].Value = "%";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe].Value = "Итоговый балл";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe].Value = "Статус";

        FormatHeader(worksheet);
    }

    private static void CreateHeaderPe(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = "ОУ";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithLastName].Value = "Фамилия";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFirstName].Value = "Имя";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithSex].Value = "Пол";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Pe].Value = "Класс, за который выступает";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe].Value = "%";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe].Value = "Итоговый балл";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe].Value = "Статус";

        FormatHeader(worksheet);
    }

    private static void CreateHeaderBase(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = "ОУ";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithLastName].Value = "Фамилия";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFirstName].Value = "Имя";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base].Value = "Класс, за который выступает";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithPercentage.Base].Value = "%";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithFinalScore.Base].Value = "Итоговый балл";
        worksheet.Cells[1, ExcelPassingPointsInfo.ColumnWithStatus.Base].Value = "Статус";

        FormatHeader(worksheet);
    }

    private static void FormatHeader(ExcelWorksheet worksheet)
    {
        using (var range = worksheet.Cells[1, 1, 1, 8])
        {
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }
    }

    private static void AddDataInWorkSheet(ExcelWorksheet worksheet, int row, SchoolOlympiadResultBase result,
        Subject subject)
    {
        switch (subject)
        {
            case Subject.PhysicalEducation:
                AddDataInWorkSheetPe(worksheet, row, result as PhysicalEducationResult);
                break;
            case Subject.Technology:
                AddDataInWorkSheetTechnology(worksheet, row, result as TechnologyResult);
                break;
            default:
                AddDataInWorkSheetBase(worksheet, row, result);
                break;
        }
    }

    private static void AddDataInWorkSheetPe(ExcelWorksheet worksheet, int row, PhysicalEducationResult result)
    {
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = result.School;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithLastName].Value = result.StudentName.LastName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFirstName].Value = result.StudentName.FirstName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithSex].Value = result.Sex.GetString();
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Pe].Value = result.GradeCompeting;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe].Value = result.Percentage;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe].Value = result.FinalScore;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe].Value = result.Status.GetString();
    }

    private static void AddDataInWorkSheetTechnology(ExcelWorksheet worksheet, int row, TechnologyResult result)
    {
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = result.School;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithLastName].Value = result.StudentName.LastName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFirstName].Value = result.StudentName.FirstName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base].Value = result.GradeCompeting;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithPractice].Value = result.DirectionPractice;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithPercentage.TechnologyOrPe].Value = result.Percentage;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFinalScore.TechnologyOrPe].Value = result.FinalScore;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithStatus.TechnologyOrPe].Value = result.Status.GetString();
    }

    private static void AddDataInWorkSheetBase(ExcelWorksheet worksheet, int row, SchoolOlympiadResultBase result)
    {
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithSchoolName].Value = result.School;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithLastName].Value = result.StudentName.LastName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFirstName].Value = result.StudentName.FirstName;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithGradeCompeting.Base].Value = result.GradeCompeting;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithPercentage.Base].Value = result.Percentage;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithFinalScore.Base].Value = result.FinalScore;
        worksheet.Cells[row, ExcelPassingPointsInfo.ColumnWithStatus.Base].Value = result.Status.GetString();
    }

    private async Task<IReadOnlyCollection<IGrouping<int, SchoolOlympiadResultBase>>> GetResults(Subject subject,
        CancellationToken cancellationToken)
    {
        return subject switch
        {
            Subject.Art => await _resultRepository.FindWithGroupByGradeCompeting<ArtResult>(
                cancellationToken: cancellationToken),
            Subject.Astronomy => await _resultRepository.FindWithGroupByGradeCompeting<AstronomyResult>(
                cancellationToken: cancellationToken),
            Subject.Biology => await _resultRepository.FindWithGroupByGradeCompeting<BiologyResult>(
                cancellationToken: cancellationToken),
            Subject.Chemistry => await _resultRepository.FindWithGroupByGradeCompeting<ChemistryResult>(
                cancellationToken: cancellationToken),
            Subject.Chinese => await _resultRepository.FindWithGroupByGradeCompeting<ChineseResult>(
                cancellationToken: cancellationToken),
            Subject.ComputerScience => await _resultRepository.FindWithGroupByGradeCompeting<ComputerScienceResult>(
                cancellationToken: cancellationToken),
            Subject.Ecology => await _resultRepository.FindWithGroupByGradeCompeting<EcologyResult>(
                cancellationToken: cancellationToken),
            Subject.Economy => await _resultRepository.FindWithGroupByGradeCompeting<EconomyResult>(
                cancellationToken: cancellationToken),
            Subject.English => await _resultRepository.FindWithGroupByGradeCompeting<EnglishResult>(
                cancellationToken: cancellationToken),
            Subject.French => await _resultRepository.FindWithGroupByGradeCompeting<FrenchResult>(
                cancellationToken: cancellationToken),
            Subject.FundamentalsLifeSafety => await _resultRepository
                .FindWithGroupByGradeCompeting<FundamentalsLifeSafetyResult>(cancellationToken: cancellationToken),
            Subject.Geography => await _resultRepository.FindWithGroupByGradeCompeting<GeographyResult>(
                cancellationToken: cancellationToken),
            Subject.German => await _resultRepository.FindWithGroupByGradeCompeting<GermanResult>(
                cancellationToken: cancellationToken),
            Subject.History => await _resultRepository.FindWithGroupByGradeCompeting<HistoryResult>(
                cancellationToken: cancellationToken),
            Subject.Law => await _resultRepository.FindWithGroupByGradeCompeting<LawResult>(
                cancellationToken: cancellationToken),
            Subject.Literature => await _resultRepository.FindWithGroupByGradeCompeting<LiteratureResult>(
                cancellationToken: cancellationToken),
            Subject.Math => await _resultRepository.FindWithGroupByGradeCompeting<MathResult>(
                cancellationToken: cancellationToken),
            Subject.PhysicalEducation => await _resultRepository.FindWithGroupByGradeCompeting<PhysicalEducationResult>(
                cancellationToken: cancellationToken),
            Subject.Physic => await _resultRepository.FindWithGroupByGradeCompeting<PhysicResult>(
                cancellationToken: cancellationToken),
            Subject.Russian => await _resultRepository.FindWithGroupByGradeCompeting<RussianResult>(
                cancellationToken: cancellationToken),
            Subject.SocialStudies => await _resultRepository.FindWithGroupByGradeCompeting<SocialStudiesResult>(
                cancellationToken: cancellationToken),
            Subject.Technology => await _resultRepository.FindWithGroupByGradeCompeting<TechnologyResult>(
                cancellationToken: cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(subject), subject, null)
        };
    }
}