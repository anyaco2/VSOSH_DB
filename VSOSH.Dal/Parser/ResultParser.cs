using System.Globalization;
using OfficeOpenXml;
using VSOSH.Domain;
using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;
using VSOSH.Domain.Services;
using VSOSH.Domain.ValueObjects;
using ILogger = Serilog.ILogger;

namespace VSOSH.Dal.Parser;

public class ResultParser : IParser
{
	#region Data
	#region Static
	private static readonly ILogger Log = Serilog.Log.ForContext<ResultParser>();
	#endregion

	#region Fields
	private readonly IProtocolRepository _protocolRepository;
	private readonly IResultRepository _resultRepository;
	#endregion
	#endregion

	#region .ctor
	/// <summary>
	/// Инициализирует экземпляр <see cref="ResultParser" />.
	/// </summary>
	/// <param name="resultRepository"><see cref="IResultRepository" />.</param>
	/// <param name="protocolRepository"><see cref="IProtocolRepository" />.</param>
	/// <exception cref="ArgumentNullException">Если один из аргументов не задан.</exception>
	public ResultParser(IResultRepository resultRepository, IProtocolRepository protocolRepository)
	{
		_resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
		_protocolRepository = protocolRepository ?? throw new ArgumentNullException(nameof(protocolRepository));
		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
	}
	#endregion

	#region IParser members
	/// <inheritdoc />
	public async Task ParseAndSaveAsync(FileStream file, CancellationToken cancellationToken = default)
	{
		List<ExcelWorksheet> worksheets;
		using var package = new ExcelPackage(file);
		try
		{
			worksheets = package.Workbook.Worksheets.ToList();
		}
		catch (Exception ex)
		{
			Log.Error($"{ex.Message} {ex.InnerException?.Message}");
			throw;
		}

		var fileInfo = new FileInfo(file.Name);
		var protocol = new Protocol(Guid.NewGuid(), fileInfo.Name);
		_protocolRepository.Add(protocol);
		var olympiadResultBases = ParseData(worksheets, protocol);
		_resultRepository.AddRange(olympiadResultBases);

		try
		{
			await _resultRepository.SaveChangesAsync(cancellationToken);
			Log.Information($"Данные из протокола {fileInfo.Name} успешно добавлены.");
		}
		catch (Exception ex)
		{
			Log.Error($"Произошла ошибка при добавлении данных из протокола {fileInfo.Name} в базу данных. {ex.Message}", ex);
		}
	}
	#endregion

	#region Private
	private SchoolOlympiadResultBase CreateOlympiadResult(string nameSubject, ExcelWorksheet worksheet, int row, Protocol protocol)
	{
		ResultBase? result = null;
		if (nameSubject != "труды" && nameSubject != "физическая культура")
		{
			result = CreateResultBase(worksheet, row);
		}

		return nameSubject switch
		{
			"математика" => new MathResult(result!.Id,
										   protocol.Id,
										   result.School,
										   result.ParticipantCode,
										   result.StudentName,
										   result.Status,
										   result.Percentage,
										   result.FinalScore,
										   result.GradeCompeting,
										   result.CurrentCompeting),
			"русский язык" => new RussianResult(result!.Id,
												protocol.Id,
												result.School,
												result.ParticipantCode,
												result.StudentName,
												result.Status,
												result.Percentage,
												result.FinalScore,
												result.GradeCompeting,
												result.CurrentCompeting),
			"география" => new GeographyResult(result!.Id,
											   protocol.Id,
											   result.School,
											   result.ParticipantCode,
											   result.StudentName,
											   result.Status,
											   result.Percentage,
											   result.FinalScore,
											   result.GradeCompeting,
											   result.CurrentCompeting),
			"литература" => new LiteratureResult(result!.Id,
												 protocol.Id,
												 result.School,
												 result.ParticipantCode,
												 result.StudentName,
												 result.Status,
												 result.Percentage,
												 result.FinalScore,
												 result.GradeCompeting,
												 result.CurrentCompeting),
			"английский язык" => new EnglishResult(result!.Id,
												   protocol.Id,
												   result.School,
												   result.ParticipantCode,
												   result.StudentName,
												   result.Status,
												   result.Percentage,
												   result.FinalScore,
												   result.GradeCompeting,
												   result.CurrentCompeting),
			"испанский язык" => new SpanishResult(result!.Id,
												  protocol.Id,
												  result.School,
												  result.ParticipantCode,
												  result.StudentName,
												  result.Status,
												  result.Percentage,
												  result.FinalScore,
												  result.GradeCompeting,
												  result.CurrentCompeting),
			"основы безопасности и защиты родины" => new FundamentalsLifeSafetyResult(result!.Id,
																					  protocol.Id,
																					  result.School,
																					  result.ParticipantCode,
																					  result.StudentName,
																					  result.Status,
																					  result.Percentage,
																					  result.FinalScore,
																					  result.GradeCompeting,
																					  result.CurrentCompeting),
			"труды" => CreateTechnologyResult(worksheet, row, protocol),
			"физическая культура" => CreatePhysicalEducationResult(worksheet, row, protocol),
			"китайский язык" => new ChineseResult(result!.Id,
												  protocol.Id,
												  result.School,
												  result.ParticipantCode,
												  result.StudentName,
												  result.Status,
												  result.Percentage,
												  result.FinalScore,
												  result.GradeCompeting,
												  result.CurrentCompeting),
			"немецкий язык" => new GermanResult(result!.Id,
												protocol.Id,
												result.School,
												result.ParticipantCode,
												result.StudentName,
												result.Status,
												result.Percentage,
												result.FinalScore,
												result.GradeCompeting,
												result.CurrentCompeting),
			"астрономия" => new AstronomyResult(result!.Id,
												protocol.Id,
												result.School,
												result.ParticipantCode,
												result.StudentName,
												result.Status,
												result.Percentage,
												result.FinalScore,
												result.GradeCompeting,
												result.CurrentCompeting),
			"биология" => new BiologyResult(result!.Id,
											protocol.Id,
											result.School,
											result.ParticipantCode,
											result.StudentName,
											result.Status,
											result.Percentage,
											result.FinalScore,
											result.GradeCompeting,
											result.CurrentCompeting),
			"информатика" => new ComputerScienceResult(result!.Id,
													   protocol.Id,
													   result.School,
													   result.ParticipantCode,
													   result.StudentName,
													   result.Status,
													   result.Percentage,
													   result.FinalScore,
													   result.GradeCompeting,
													   result.CurrentCompeting),
			"история" => new HistoryResult(result!.Id,
										   protocol.Id,
										   result.School,
										   result.ParticipantCode,
										   result.StudentName,
										   result.Status,
										   result.Percentage,
										   result.FinalScore,
										   result.GradeCompeting,
										   result.CurrentCompeting),
			"искусство (мхк)" => new ArtResult(result!.Id,
											   protocol.Id,
											   result.School,
											   result.ParticipantCode,
											   result.StudentName,
											   result.Status,
											   result.Percentage,
											   result.FinalScore,
											   result.GradeCompeting,
											   result.CurrentCompeting),
			"обществознание" => new SocialStudiesResult(result!.Id,
														protocol.Id,
														result.School,
														result.ParticipantCode,
														result.StudentName,
														result.Status,
														result.Percentage,
														result.FinalScore,
														result.GradeCompeting,
														result.CurrentCompeting),
			"право" => new LawResult(result!.Id,
									 protocol.Id,
									 result.School,
									 result.ParticipantCode,
									 result.StudentName,
									 result.Status,
									 result.Percentage,
									 result.FinalScore,
									 result.GradeCompeting,
									 result.CurrentCompeting),
			"физика" => new PhysicResult(result!.Id,
										 protocol.Id,
										 result.School,
										 result.ParticipantCode,
										 result.StudentName,
										 result.Status,
										 result.Percentage,
										 result.FinalScore,
										 result.GradeCompeting,
										 result.CurrentCompeting),
			"французский язык" => new FrenchResult(result!.Id,
												   protocol.Id,
												   result.School,
												   result.ParticipantCode,
												   result.StudentName,
												   result.Status,
												   result.Percentage,
												   result.FinalScore,
												   result.GradeCompeting,
												   result.CurrentCompeting),
			"экология" => new EcologyResult(result!.Id,
											protocol.Id,
											result.School,
											result.ParticipantCode,
											result.StudentName,
											result.Status,
											result.Percentage,
											result.FinalScore,
											result.GradeCompeting,
											result.CurrentCompeting),
			"экономика" => new EconomyResult(result!.Id,
											 protocol.Id,
											 result.School,
											 result.ParticipantCode,
											 result.StudentName,
											 result.Status,
											 result.Percentage,
											 result.FinalScore,
											 result.GradeCompeting,
											 result.CurrentCompeting),
			"химия" => new ChemistryResult(result!.Id,
										   protocol.Id,
										   result.School,
										   result.ParticipantCode,
										   result.StudentName,
										   result.Status,
										   result.Percentage,
										   result.FinalScore,
										   result.GradeCompeting,
										   result.CurrentCompeting),
			_ => throw new ArgumentException($"Неизвестная дисциплина {nameSubject}.")
		};
	}

	private PhysicalEducationResult CreatePhysicalEducationResult(ExcelWorksheet worksheet, int row, Protocol protocol)
	{
		var school = worksheet.Cells[row, ExcelResultInfo.ColumnWithSchoolName]
							  .GetValue<string>();
		var participantCode = worksheet.Cells[row, ExcelResultInfo.ColumnWithParticipantCode]
									   .GetValue<string>();
		var lastName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentLastName]
								.GetValue<string>();
		var firstName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentFirstName]
								 .GetValue<string>();
		var middleName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentMiddleName]
								  .GetValue<string>();
		var student = new StudentName
		{
			FirstName = firstName,
			LastName = lastName,
			MiddleName = middleName
		};
		var gradeCompeting = int.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithGradeCompeting.PEGradeCompeting]
												.GetValue<string>());
		var currentGradeInString = worksheet.Cells[row, ExcelResultInfo.ColumnWithCurrentCompeting.PECurrentCompeting]
											.GetValue<string>();
		int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
		var finalScore = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithFinalScore.PEFinalScore]
											   .GetValue<string>(),
									  CultureInfo.CurrentCulture);
		var percentageInDouble = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithPercentage.PEPercentage]
													   .GetValue<string>(),
											  CultureInfo.CurrentCulture);
		var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
		var status = GetStatus(worksheet.Cells[row, ExcelResultInfo.ColumnWithStatus.PEStatus]
										.GetValue<string>());
		var preTheory = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithPreliminaryScoreInTheory]
											  .GetValue<string>(),
									 CultureInfo.CurrentCulture);
		var finalTheory = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithFinalScoreInTheory]
												.GetValue<string>(),
									   CultureInfo.CurrentCulture);
		var prePractice = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithPreliminaryScoreInPractice]
												.GetValue<string>(),
									   CultureInfo.CurrentCulture);
		var finalPractice = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithFinalScoreInPractice]
												  .GetValue<string>(),
										 CultureInfo.CurrentCulture);
		var sex = GetSex(worksheet.Cells[row, ExcelResultInfo.ColumnWithSexStudent]
								  .GetValue<string>());
		return new PhysicalEducationResult(Guid.NewGuid(),
										   protocol.Id,
										   school,
										   participantCode,
										   student,
										   status,
										   percentage,
										   finalScore,
										   gradeCompeting,
										   currentGrade,
										   preTheory,
										   finalTheory,
										   prePractice,
										   finalPractice,
										   sex);
	}

	private ResultBase CreateResultBase(ExcelWorksheet worksheet, int row)
	{
		var school = worksheet.Cells[row, ExcelResultInfo.ColumnWithSchoolName]
							  .GetValue<string>();
		var participantCode = worksheet.Cells[row, ExcelResultInfo.ColumnWithParticipantCode]
									   .GetValue<string>();
		var lastName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentLastName]
								.GetValue<string>();
		var firstName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentFirstName]
								 .GetValue<string>();
		var middleName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentMiddleName]
								  .GetValue<string>();
		var student = new StudentName
		{
			FirstName = firstName,
			LastName = lastName,
			MiddleName = middleName
		};
		var gradeCompeting = int.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithGradeCompeting.BaseGradeCompeting]
												.GetValue<string>());
		var currentGradeInString = worksheet.Cells[row, ExcelResultInfo.ColumnWithCurrentCompeting.BaseCurrentCompeting]
											.GetValue<string>();
		int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
		var finalScore = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithFinalScore.BaseFinalScore]
											   .GetValue<string>(),
									  CultureInfo.CurrentCulture);
		var percentageInDouble = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithPercentage.BasePercentage]
													   .GetValue<string>(),
											  CultureInfo.CurrentCulture);
		var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
		var status = GetStatus(worksheet.Cells[row, ExcelResultInfo.ColumnWithStatus.BaseStatus]
										.GetValue<string>());
		return new ResultBase(Guid.NewGuid(), school, participantCode, student, status, percentage, finalScore, gradeCompeting, currentGrade);
	}

	private TechnologyResult CreateTechnologyResult(ExcelWorksheet worksheet, int row, Protocol protocol)
	{
		var school = worksheet.Cells[row, ExcelResultInfo.ColumnWithSchoolName]
							  .GetValue<string>();
		var participantCode = worksheet.Cells[row, ExcelResultInfo.ColumnWithParticipantCode]
									   .GetValue<string>();
		var lastName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentLastName]
								.GetValue<string>();
		var firstName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentFirstName]
								 .GetValue<string>();
		var middleName = worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentMiddleName]
								  .GetValue<string>();
		var student = new StudentName
		{
			FirstName = firstName,
			LastName = lastName,
			MiddleName = middleName
		};
		var gradeCompeting = int.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithGradeCompeting.BaseGradeCompeting]
												.GetValue<string>());
		var currentGradeInString = worksheet.Cells[row, ExcelResultInfo.ColumnWithCurrentCompeting.BaseCurrentCompeting]
											.GetValue<string>();
		int? currentGrade = string.IsNullOrWhiteSpace(currentGradeInString) ? null : int.Parse(currentGradeInString);
		var finalScore = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithFinalScore.TechnologyFinalScore]
											   .GetValue<string>(),
									  CultureInfo.CurrentCulture);
		var percentageInDouble = double.Parse(worksheet.Cells[row, ExcelResultInfo.ColumnWithPercentage.TechnologyPercentage]
													   .GetValue<string>(),
											  CultureInfo.CurrentCulture);
		var percentage = int.Parse($"{Math.Round(percentageInDouble * 100)}");
		var status = GetStatus(worksheet.Cells[row, ExcelResultInfo.ColumnWithStatus.TechnologyStatus]
										.GetValue<string>());
		var directionPractise = worksheet.Cells[row, ExcelResultInfo.ColumnWithDirectionPractice]
										 .GetValue<string>();
		return new TechnologyResult(Guid.NewGuid(), protocol.Id, school, participantCode, student, status, percentage, finalScore, gradeCompeting, currentGrade, directionPractise);
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

	private IReadOnlyCollection<SchoolOlympiadResultBase> ParseData(List<ExcelWorksheet> worksheets, Protocol protocol)
	{
		List<SchoolOlympiadResultBase> olympiadResultBases = [];
		foreach (var worksheet in worksheets.Where(worksheet => worksheet.Name is not ("Инструкция" or "Правила")))
		{
			Log.Information($"Начало обработки данных со страницы {worksheet.Name}");

			var row = ExcelResultInfo.StartRow.StartRow;
			var nameSubject = worksheet.Cells[row, ExcelResultInfo.ColumnWithNameOfSchoolSubject]
									   .GetValue<string>();

			if (nameSubject.ToLower() == "предмет")
			{
				row = ExcelResultInfo.StartRow.PeStartRow;
				nameSubject = worksheet.Cells[row, ExcelResultInfo.ColumnWithNameOfSchoolSubject]
									   .GetValue<string>();
			}

			while (!string.IsNullOrWhiteSpace(worksheet.Cells[row, ExcelResultInfo.ColumnWithStudentFirstName]
													   .GetValue<string>()))
			{
				try
				{
					olympiadResultBases.Add(CreateOlympiadResult(nameSubject.ToLower(), worksheet, row, protocol));
					row++;
				}
				catch (Exception ex)
				{
					Log.Error($"Произошла ошибка при обработке данных из протокола: {ex.Message}", ex);
					throw;
				}
			}
		}

		return olympiadResultBases;
	}
	#endregion

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
}
