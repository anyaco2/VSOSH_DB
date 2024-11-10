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
    /// Инициализирует эксемпляр класса <see cref="SchoolOlympiadResultBase" />.
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
    protected SchoolOlympiadResultBase(Guid id, 
        string school, 
        string participantCode, 
        StudentName studentName, 
        string status, 
        double percentage, 
        double finalScore, 
        int currentGrade, 
        int gradeCompeting) 
        : base(id)
    {
        School = school;
        ParticipantCode = participantCode;
        StudentName = studentName;
        Status = status;
        Percentage = percentage;
        FinalScore = finalScore;
        CurrentGrade = currentGrade;
        GradeCompeting = gradeCompeting;
    }

    #endregion
    
    #region Properties

    /// <summary>
    /// Возвращает образовательное учреждение.
    /// </summary>
    public string School { get; init; }
    
    /// <summary>
    /// Возвращает код участника.
    /// </summary>
    public string ParticipantCode { get; init; }
    
    /// <summary>
    /// Вовзращает ФИО ученика.
    /// </summary>
    public StudentName StudentName { get; init; }
    
    /// <summary>
    /// Возвращает класс, за который выступает ученик.
    /// </summary>
    public int GradeCompeting { get; init; }
    
    /// <summary>
    /// Возвращает класс, в котором учится.
    /// </summary>
    public int CurrentGrade { get; init; }
    
    /// <summary>
    /// Возвращает итоговый балл.
    /// </summary>
    public double FinalScore { get; init; }
    
    /// <summary>
    /// Возвращает статус ученика.
    /// </summary>
    public string Status { get; init; }   
    
    /// <summary>
    /// Возвращает процент выполнения.
    /// </summary>
    public double Percentage { get; init; }

    #endregion
}