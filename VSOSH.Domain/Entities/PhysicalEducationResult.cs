using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результат олимпиады по физкультуре.
/// </summary>
public class PhysicalEducationResult : SchoolOlympiadResultBase
{
	#region .ctor
	/// <summary>
	/// Инициализирует экземпляр класса <see cref="PhysicalEducationResult" />.
	/// </summary>
	/// <param name="id">Идентификатор результата.</param>
	/// <param name="protocolId">Идентификатор протокола.</param>
	/// <param name="school">Образовательное учреждение.</param>
	/// <param name="participantCode">Код участника.</param>
	/// <param name="studentName">ФИО ученика.</param>
	/// <param name="status">Статус ученика.</param>
	/// <param name="percentage">Процент выполнения.</param>
	/// <param name="finalScore">Итоговый балл.</param>
	/// <param name="gradeCompeting">Класс, в котором учится.</param>
	/// <param name="currentCompeting">Класс, за который выступает.</param>
	/// <param name="preliminaryScoreInTheory">Предварительный балл по теории.</param>
	/// <param name="finalScoreInTheory">Итоговый балл по теории.</param>
	/// <param name="preliminaryScoreInPractice">Предварительный балл по практике</param>
	/// <param name="finalScoreInPractice">Итоговый балл по практике.</param>
	/// <param name="sex">Пол ученика.</param>
	public PhysicalEducationResult(Guid id,
								   Guid protocolId,
								   string school,
								   string participantCode,
								   StudentName studentName,
								   Status status,
								   double percentage,
								   double finalScore,
								   int gradeCompeting,
								   int? currentCompeting,
								   double preliminaryScoreInTheory,
								   double finalScoreInTheory,
								   double preliminaryScoreInPractice,
								   double finalScoreInPractice,
								   Sex sex)
		: base(id, protocolId, school, participantCode, studentName, status, percentage, finalScore, gradeCompeting, currentCompeting)
	{
		PreliminaryScoreInTheory = preliminaryScoreInTheory;
		FinalScoreInTheory = finalScoreInTheory;
		PreliminaryScoreInPractice = preliminaryScoreInPractice;
		FinalScoreInPractice = finalScoreInPractice;
		Sex = sex;
	}

	private PhysicalEducationResult()
	{
	}
	#endregion

	#region Properties
	/// <summary>
	/// Возвращает итоговый балл по практике.
	/// </summary>
	public double FinalScoreInPractice
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает итоговый балл по теории.
	/// </summary>
	public double FinalScoreInTheory
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает предварительный балл по практике.
	/// </summary>
	public double PreliminaryScoreInPractice
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает предварительный балл по теории.
	/// </summary>
	public double PreliminaryScoreInTheory
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает пол ученика.
	/// </summary>
	public Sex Sex
	{
		get;
		init;
	}
	#endregion
}
