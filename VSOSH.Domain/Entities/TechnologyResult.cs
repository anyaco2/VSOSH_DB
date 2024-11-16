using VSOSH.Domain.ValueObjects;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет результат олимпиады по технологии.
/// </summary>
public class TechnologyResult : SchoolOlympiadResultBase
{
    #region Properties

    /// <summary>
    /// Возвращает направление практики.
    /// </summary>
    public string DirectionPractice { get; init; }

    #endregion

    #region .ctor

    /// <summary>
    /// Инициализирует эксемпляр класса <see cref="TechnologyResult" />.
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
    /// <param name="directionPractice">Направление практики.</param>
    public TechnologyResult(Guid id, string school, string participantCode, StudentName studentName, Status status,
        double percentage, double finalScore, int gradeCompeting, int? currentCompeting, string directionPractice)
        : base(id, school, participantCode, studentName, status, percentage, finalScore, gradeCompeting,
            currentCompeting)
    {
        DirectionPractice = directionPractice;
    }

    private TechnologyResult()
    {
    }

    #endregion
}