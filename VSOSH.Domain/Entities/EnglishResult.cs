using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результат олимпиады по английскому языку.
/// </summary>
public class EnglishResult : SchoolOlympiadResultBase
{
    #region .ctor

    /// <summary>
    /// Инициализирует эксемпляр класса <see cref="EnglishResult" />.
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
    public EnglishResult(Guid id, string school, string participantCode, StudentName studentName, Status status,
        double percentage, double finalScore, int gradeCompeting, int? currentCompeting)
        : base(id, school, participantCode, studentName, status, percentage, finalScore, gradeCompeting,
            currentCompeting)
    {
    }

    private EnglishResult()
    {
    }

    #endregion
}