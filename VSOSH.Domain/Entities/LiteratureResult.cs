﻿using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результат олимпиады по литературе.
/// </summary>
public class LiteratureResult : SchoolOlympiadResultBase
{
	#region .ctor
	/// <summary>
	/// Инициализирует экземпляр класса <see cref="LiteratureResult" />.
	/// </summary>
	/// <param name="id">Идентификатор результата.</param>
	/// <param name="school">Образовательное учреждение.</param>
	/// <param name="participantCode">Код участника.</param>
	/// <param name="studentName">ФИО ученика.</param>
	/// <param name="status">Статус ученика.</param>
	/// <param name="percentage">Процент выполнения.</param>
	/// <param name="finalScore">Итоговый балл.</param>
	/// <param name="gradeCompeting">Класс, в котором учится.</param>
	/// <param name="currentCompeting">Класс, за который выступает.</param>
	public LiteratureResult(Guid id,
							Guid protocolId,
							string school,
							string participantCode,
							StudentName studentName,
							Status status,
							double percentage,
							double finalScore,
							int gradeCompeting,
							int? currentCompeting)
		: base(id, protocolId, school, participantCode, studentName, status, percentage, finalScore, gradeCompeting, currentCompeting)
	{
	}

	private LiteratureResult()
	{
	}
	#endregion
}
