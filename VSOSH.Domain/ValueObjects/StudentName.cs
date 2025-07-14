using CSharpFunctionalExtensions;

namespace VSOSH.Domain.ValueObjects;

/// <summary>
/// Представляет ФИО ученика.
/// </summary>
public class StudentName : ValueObject
{
	#region Properties
	/// <summary>
	/// Возвращает имя ученика.
	/// </summary>
	public required string FirstName
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает фамилию ученика.
	/// </summary>
	public required string LastName
	{
		get;
		init;
	}

	/// <summary>
	/// Возвращает отчество ученика.
	/// </summary>
	public string? MiddleName
	{
		get;
		init;
	}
	#endregion

	#region Overrided
	/// <inheritdoc />
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return FirstName;
		yield return LastName;
		yield return MiddleName;
	}
	#endregion
}
