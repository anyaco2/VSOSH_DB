using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результат олимпиады по истории.
/// </summary>
public class HistoryResult : SchoolOlympiadResultBase
{
    #region .ctor

    /// <summary>
    /// Инициализирует эксемпляр класса <see cref="HistoryResult" />.
    /// </summary>
    /// <param name="id">Идентификатор результата.</param>
    /// <param name="school">Образовательное учреждение.</param>
    /// <param name="participantCode">Код участника.</param>
    /// <param name="studentName">ФИО ученика.</param>
    /// <param name="status">Статус ученика.</param>
    /// <param name="percentage">Процент выполнения.</param>
    /// <param name="finalScore">Итоговый балл.</param>
    /// <param name="currentGrade">Класс, в котором учится.</param>
    /// <param name="gradeCompeting">Класс, за который выступает.</param>
    public HistoryResult(Guid id, string school, string participantCode, StudentName studentName, Status status,
        double percentage, double finalScore, int currentGrade, int? gradeCompeting)
        : base(id, school, participantCode, studentName, status, percentage, finalScore, currentGrade, gradeCompeting)
    {
    }

    private HistoryResult()
    {
    }

    #endregion
}