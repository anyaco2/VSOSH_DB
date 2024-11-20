using System.Globalization;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using VSOSH.Domain;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using VSOSH.Domain.ValueObjects;

namespace VSOSH.Dal.Parser;

public class ResultParser : IParser
{
    #region .ctor

    /// <summary>
    /// Инициализирует экземпляр <see cref="ResultParser" />.
    /// </summary>
    /// <param name="resultRepository"><see cref="IResultRepository" />.</param>
    /// <param name="logger"><see cref="ILogger{ResultParser}" />.</param>
    /// <exception cref="ArgumentNullException">Если один из аргументов не задан.</exception>
    public ResultParser(IResultRepository resultRepository, ILogger<ResultParser> logger)
    {
        _resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Public

    /// <inheritdoc />
    public async Task ParseAndSaveAsync(Stream file, CancellationToken cancellationToken = default)
    {
        file.Position = 0;
        var worksheets = new XLWorkbook(file).Worksheets.ToList().Skip(1);
        foreach (var worksheet in worksheets)
        {
            _logger.LogInformation($"Начало обработки парсинга данных со страницы {worksheet.Name}");
            var row = ExcelInfo.StartRow.StartRow;
            var nameSubject = worksheet.Cell(row, ExcelInfo.ColumnWithNameOfSchoolSubject).GetString();
            List<SchoolOlympiadResultBase> olympiadResultBases = [];
            if (nameSubject.ToLower() == "предмет")
            {
                row = ExcelInfo.StartRow.PeStartRow;
                nameSubject = worksheet.Cell(row, ExcelInfo.ColumnWithNameOfSchoolSubject).GetString();
            }

            while (!string.IsNullOrWhiteSpace(worksheet.Cell(row, ExcelInfo.ColumnWithStudentFirstName).GetString()))
            {
                try
                {
                    var excelRow = worksheet.Row(row);
                    olympiadResultBases.Add(CreateOlympiadResult(nameSubject.ToLower(), excelRow));
                    row++;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Произошла ошибка при парсинге данных: {ex.Message}", ex);
                }
            }

            if (olympiadResultBases.Count == 0)
            {
                continue;
            }

            try
            {
                await _resultRepository.AddRangeAsync(olympiadResultBases, cancellationToken);
                _logger.LogInformation($"Данные со страницы {worksheet.Name} успешно добавлены.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Произошла ошибка при добавлении данных со страницы {worksheet.Name} в Базу данных. {ex.Message}",
                    ex);
            }
        }
    }

    #endregion

    #region Data

    private readonly IResultRepository _resultRepository;
    private readonly ILogger<ResultParser> _logger;

    #endregion

    #region Private

    private SchoolOlympiadResultBase CreateOlympiadResult(string nameSubject, IXLRow row)
    {
        ResultBase result = null;
        if (nameSubject != "труды" && nameSubject != "физическая культура")
        {
            result = CreateResultBase(row);
        }

        return nameSubject switch
        {
            "математика" => new MathResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "русский язык" => new RussianResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "география" => new GeographyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "литература" => new LiteratureResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "английский язык" => new EnglishResult(result!.Id, result.School, result.ParticipantCode,
                result.StudentName, result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                result.CurrentCompeting),
            "основы безопасности и защиты родины" => new FundamentalsLifeSafetyResult(result!.Id, result.School,
                result.ParticipantCode, result.StudentName, result.Status, result.Percentage, result.FinalScore,
                result.GradeCompeting, result.CurrentCompeting),
            "труды" => CreateTechnologyResult(row),
            "физическая культура" => CreatePhysicalEducationResult(row),
            "китайский язык" => new ChineseResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "немецкий язык" => new GermanResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "астрономия" => new AstronomyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "биология" => new BiologyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "информатика" => new ComputerScienceResult(result!.Id, result.School, result.ParticipantCode,
                result.StudentName, result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                result.CurrentCompeting),
            "история" => new HistoryResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "искусство (мхк)" => new ArtResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "обществознание" => new SocialStudiesResult(result!.Id, result.School, result.ParticipantCode,
                result.StudentName, result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                result.CurrentCompeting),
            "право" => new LawResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "физика" => new PhysicResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "французский язык" => new FrenchResult(result!.Id, result.School, result.ParticipantCode,
                result.StudentName, result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                result.CurrentCompeting),
            "экология" => new EcologyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "экономика" => new EconomyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            "химия" => new ChemistryResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                result.Status, result.Percentage, result.FinalScore, result.GradeCompeting, result.CurrentCompeting),
            _ => throw new ArgumentException($"Неизвестный дисциплина {nameSubject}.")
        };
    }

    private ResultBase CreateResultBase(IXLRow row)
    {
        var school = row.Cell(ExcelInfo.ColumnWithSchoolName).GetString();
        var participantCode = row.Cell(ExcelInfo.ColumnWithParticipantCode).GetString();
        var lastName = row.Cell(ExcelInfo.ColumnWithStudentLastName).GetString();
        var firstName = row.Cell(ExcelInfo.ColumnWithStudentFirstName).GetString();
        var middleName = row.Cell(ExcelInfo.ColumnWithStudentMiddleName).GetString();
        var student = new StudentName()
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName
        };
        var gradeCompeting = int.Parse(row.Cell(ExcelInfo.ColumnWithGradeCompeting.BaseGradeCompeting).GetString());
        var currentGradeInString = row.Cell(ExcelInfo.ColumnWithCurrentCompeting.BaseCurrentCompeting).GetString();
        int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
        var finalScore = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScore.BaseFinalScore).GetString(),
            CultureInfo.CurrentCulture);
        var percentageInDouble =
            double.Parse(row.Cell(ExcelInfo.ColumnWithPercentage.BasePercentage).GetString(),
                CultureInfo.CurrentCulture);
        var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
        var status = GetStatus(row.Cell(ExcelInfo.ColumnWithStatus.BaseStatus).GetString());
        return new ResultBase(Guid.NewGuid(), school, participantCode, student, status, percentage,
            finalScore, gradeCompeting, currentGrade);
    }

    private PhysicalEducationResult CreatePhysicalEducationResult(IXLRow row)
    {
        var school = row.Cell(ExcelInfo.ColumnWithSchoolName).GetString();
        var participantCode = row.Cell(ExcelInfo.ColumnWithParticipantCode).GetString();
        var lastName = row.Cell(ExcelInfo.ColumnWithStudentLastName).GetString();
        var firstName = row.Cell(ExcelInfo.ColumnWithStudentFirstName).GetString();
        var middleName = row.Cell(ExcelInfo.ColumnWithStudentMiddleName).GetString();
        var student = new StudentName()
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName
        };
        var gradeCompeting = int.Parse(row.Cell(ExcelInfo.ColumnWithGradeCompeting.PEGradeCompeting).GetString());
        var currentGradeInString = row.Cell(ExcelInfo.ColumnWithCurrentCompeting.PECurrentCompeting).GetString();
        int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
        var finalScore = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScore.PEFinalScore).GetString(),
            CultureInfo.CurrentCulture);
        var percentageInDouble =
            double.Parse(row.Cell(ExcelInfo.ColumnWithPercentage.PEPercentage).GetString(), CultureInfo.CurrentCulture);
        var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
        var status = GetStatus(row.Cell(ExcelInfo.ColumnWithStatus.PEStatus).GetString());
        var preTheory = double.Parse(row.Cell(ExcelInfo.ColumnWithPreliminaryScoreInTheory).GetString(),
            CultureInfo.CurrentCulture);
        var finalTheory = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScoreInTheory).GetString(),
            CultureInfo.CurrentCulture);
        var prePractice = double.Parse(row.Cell(ExcelInfo.ColumnWithPreliminaryScoreInPractice).GetString(),
            CultureInfo.CurrentCulture);
        var finalPractice = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScoreInPractice).GetString(),
            CultureInfo.CurrentCulture);
        var sex = GetSex(row.Cell(ExcelInfo.ColumnWithSexStudent).GetString());
        return new PhysicalEducationResult(Guid.NewGuid(), school, participantCode, student, status, percentage,
            finalScore, gradeCompeting, currentGrade, preTheory, finalTheory, prePractice, finalPractice, sex);
    }

    private TechnologyResult CreateTechnologyResult(IXLRow row)
    {
        var school = row.Cell(ExcelInfo.ColumnWithSchoolName).GetString();
        var participantCode = row.Cell(ExcelInfo.ColumnWithParticipantCode).GetString();
        var lastName = row.Cell(ExcelInfo.ColumnWithStudentLastName).GetString();
        var firstName = row.Cell(ExcelInfo.ColumnWithStudentFirstName).GetString();
        var middleName = row.Cell(ExcelInfo.ColumnWithStudentMiddleName).GetString();
        var student = new StudentName()
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName
        };
        var gradeCompeting = int.Parse(row.Cell(ExcelInfo.ColumnWithGradeCompeting.BaseGradeCompeting).GetString());
        var currentGradeInString = row.Cell(ExcelInfo.ColumnWithCurrentCompeting.BaseCurrentCompeting).GetString();
        int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
        var finalScore = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScore.TechnologyFinalScore).GetString(),
            CultureInfo.CurrentCulture);
        var percentageInDouble =
            double.Parse(row.Cell(ExcelInfo.ColumnWithPercentage.TechnologyPercentage).GetString(),
                CultureInfo.CurrentCulture);
        var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
        var status = GetStatus(row.Cell(ExcelInfo.ColumnWithStatus.TechnologyStatus).GetString());
        var directionPractise = row.Cell(ExcelInfo.ColumnWithDirectionPractice).GetString();
        return new TechnologyResult(Guid.NewGuid(), school, participantCode, student, status, percentage,
            finalScore, gradeCompeting, currentGrade, directionPractise);
    }

    private Status GetStatus(string status)
    {
        return status.ToLower() switch
        {
            "призер" => Status.Awardee,
            "победитель" => Status.Winner,
            "участник" => Status.Participant,
            _ => throw new ArgumentException($"Неизвестный тип статуса: {status}.")
        };
    }

    private Sex GetSex(string sex)
    {
        return sex.ToLower() switch
        {
            "м" => Sex.Male,
            "ж" => Sex.Female,
            _ => throw new ArgumentException($"Неизвестный пол: {sex} ")
        };
    }

    private record ResultBase(
        Guid Id,
        string School,
        string ParticipantCode,
        StudentName StudentName,
        Status Status,
        double Percentage,
        double FinalScore,
        int GradeCompeting,
        int? CurrentCompeting);

    #endregion
}