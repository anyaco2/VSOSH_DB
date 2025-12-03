using CSharpFunctionalExtensions;
using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результаты по олимпиаде.
/// </summary>
public abstract class SchoolOlympiadResultBase : Entity<Guid>
{
	#region .ctor
	/// <summary>
	/// Инициализирует экземпляр класса <see cref="SchoolOlympiadResultBase" />.
	/// </summary>
	/// <param name="id">Идентификатор результата.</param>
	/// <param name="protocolId">Идентификатор протокола.</param>
	/// <param name="school">Образовательное учреждение.</param>
	/// <param name="participantCode">Код участника.</param>
	/// <param name="studentName">ФИО ученика.</param>
	/// <param name="status">Статус ученика.</param>
	/// <param name="percentage">Процент выполнения.</param>
	/// <param name="finalScore">Итоговый балл.</param>
	/// <param name="gradeCompeting">Класс, за который выступает.</param>
	/// <param name="currentCompeting">Класс, в котором учится.</param>
	protected SchoolOlympiadResultBase(Guid id,
									   Guid protocolId,
									   string school,
									   string participantCode,
									   StudentName studentName,
									   Status status,
									   double percentage,
									   double finalScore,
									   int gradeCompeting,
									   int? currentCompeting)
		: base(id)
	{
		School = school;
		ParticipantCode = participantCode;
		StudentName = studentName;
		Status = status;
		Percentage = percentage;
		FinalScore = finalScore;
		GradeCompeting = gradeCompeting;
		CurrentCompeting = currentCompeting;
		ProtocolId = protocolId;
	}

	/// <summary>
	/// Конструктор для ef-core.
	/// </summary>
	protected SchoolOlympiadResultBase()
	{
	}
	#endregion

	#region Properties
	/// <summary>
	/// Возвращает класс, в котором учится.
	/// </summary>
	public int? CurrentCompeting
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает итоговый балл.
	/// </summary>
	public double FinalScore
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает класс, за который выступает.
	/// </summary>
	public int GradeCompeting
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает код участника.
	/// </summary>
	public string ParticipantCode
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает процент выполнения.
	/// </summary>
	public double Percentage
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает идентификатор протокола.
	/// </summary>
	public Guid ProtocolId
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает образовательное учреждение.
	/// </summary>
	public string School
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает статус ученика.
	/// </summary>
	public Status Status
	{
		get;
		init;
	}

	/// <summary>
	/// Вовзращает ФИО ученика.
	/// </summary>
	public StudentName StudentName
	{
		get;
		init;
	}
	#endregion

	#region Public
	/// <summary>
	/// Возвращает строковое представление типа результата.
	/// </summary>
	/// <returns>Название предмета в строковом представлении.</returns>
	public string GetResultName() =>
		this switch
		{
			ArtResult => "искусство (МХК)",
			AstronomyResult => "астрономия",
			BiologyResult => "биология",
			ChemistryResult => "химия",
			ChineseResult => "китайский язык",
			ComputerScienceResult => "информатика",
			EcologyResult => "экология",
			EconomyResult => "экономика",
			EnglishResult => "английский язык",
			FrenchResult => "французский язык",
			FundamentalsLifeSafetyResult => "основы безопасности и защиты родины",
			GeographyResult => "география",
			GermanResult => "немецкий язык",
			HistoryResult => "история",
			LawResult => "право",
			LiteratureResult => "литература",
			MathResult => "математика",
			PhysicalEducationResult => "физическая культура",
			PhysicResult => "физика",
			RussianResult => "русский язык",
			SocialStudiesResult => "обществознание",
			TechnologyResult => "труды",
			SpanishResult => "испанский язык",
			ItalianResult => "итальянский язык",
			_ => throw new InvalidOperationException("Неизвестный тип результата")
		};
	#endregion
}
