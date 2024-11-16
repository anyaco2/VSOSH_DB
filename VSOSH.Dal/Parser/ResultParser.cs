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
    #region Data

    private readonly IResultRepository _resultRepository;
    private readonly ILogger<ResultParser> _logger;

    #endregion
    
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
            var row = ExcelInfo.StartRow.startRow;
            var nameSubject = worksheet.Cell(row, ExcelInfo.ColumnWithNameOfSchoolSubject).GetString();
            List<SchoolOlympiadResultBase> olympiadResultBases = [];
            if (nameSubject.ToLower() == "предмет")
            {
                row = ExcelInfo.StartRow.secondStartRow;
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

    #region Private

    private SchoolOlympiadResultBase CreateOlympiadResult(string nameSubject, IXLRow row)
    {
        ResultBase result = null;
        if (nameSubject != "труды" || nameSubject != "физическая культура")
        {
            result = CreateResultBase(row);
        }

        switch (nameSubject)
        {
            case "математика":
                return new MathResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "русский язык":
                return new RussianResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "география":
                return new GeographyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "литература":
                return new LiteratureResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "английский язык":
                return new EnglishResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "основы безопасности и защиты родины":
                return new FundamentalsLifeSafetyResult(result!.Id, result.School, result.ParticipantCode,
                    result.StudentName, result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "труды":
                //TODO: Добавить обработку трудов
                return null;
            case "физическая культура":
                //TODO: Добавить обработку физ-ры
                return null;
            case "китайский язык":
                return new ChineseResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "немецкий язык":
                return new GermanResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "астрономия":
                return new AstronomyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "биология":
                return new BiologyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "информатика":
                return new ComputerScienceResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "история":
                return new HistoryResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "искусство (мхк)":
                return new ArtResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "обществознание":
                return new SocialStudiesResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "право":
                return new LawResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "физика":
                return new PhysicResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "французский язык":
                return new FrenchResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "экология":
                return new EcologyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "экономика":
                return new EconomyResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            case "химия":
                return new ChemistryResult(result!.Id, result.School, result.ParticipantCode, result.StudentName,
                    result.Status, result.Percentage, result.FinalScore, result.GradeCompeting,
                    result.CurrentCompeting);
            default:

                return null;
        }
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
        var currentGradeInString = row.Cell(ExcelInfo.ColumnWithCurrentCompeting).GetString();
        int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
        var finalScore = double.Parse(row.Cell(ExcelInfo.ColumnWithFinalScore).GetString(), CultureInfo.CurrentCulture);
        var percentageInDouble =
            double.Parse(row.Cell(ExcelInfo.ColumnWithPercentage).GetString(), CultureInfo.CurrentCulture);
        var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
        var status = GetStatus(row.Cell(ExcelInfo.ColumnWithStatus).GetString());
        return new ResultBase(Guid.NewGuid(), school, participantCode, student, status, percentage,
            finalScore, gradeCompeting, currentGrade);
    }

    private Status GetStatus(string status)
    {
        return status.ToLower() switch
        {
            "призер" => Status.Awardee,
            "победитель" => Status.Winner,
            "участник" => Status.Participant,
            _ => throw new ArgumentException("Неизвестный тип статуса.")
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