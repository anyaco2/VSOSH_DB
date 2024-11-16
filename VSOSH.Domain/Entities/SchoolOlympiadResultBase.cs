﻿using CSharpFunctionalExtensions;
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
    /// <param name="gradeCompeting">Класс, за который выступает.</param>
    /// <param name="currentCompeting">Класс, в котором учится.</param>
    protected SchoolOlympiadResultBase(Guid id,
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
    /// Возвращает класс, в котором учится.
    /// </summary>
    public int? CurrentCompeting { get; init; }

    /// <summary>
    /// Возвращает класс, за который выступает.
    /// </summary>
    public int GradeCompeting { get; init; }

    /// <summary>
    /// Возвращает итоговый балл.
    /// </summary>
    public double FinalScore { get; init; }

    /// <summary>
    /// Возвращает статус ученика.
    /// </summary>
    public Status Status { get; init; }

    /// <summary>
    /// Возвращает процент выполнения.
    /// </summary>
    public double Percentage { get; init; }

    #endregion
}