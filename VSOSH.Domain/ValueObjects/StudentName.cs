using CSharpFunctionalExtensions;

namespace VSOSH.Domain.ValueObjects;

/// <summary>
/// Представляет ФИО ученика.
/// </summary>
public class StudentName : ValueObject
{
    /// <summary>
    /// Возвращает имя ученика.
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Возвращает фамилию ученика.
    /// </summary>
    public required string LastName { get; init; }
    
    /// <summary>
    /// Возвращает отчество ученика.
    /// </summary>
    public required string MiddleName { get; init; }
    
    /// <inheritdoc />
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}