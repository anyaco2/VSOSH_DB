using CSharpFunctionalExtensions;

namespace VSOSH.Domain.Entities;

/// <summary>
/// Представляет протокол.
/// </summary>
public class Protocol : Entity<Guid>
{
	#region .ctor
	/// <summary>
	/// Инициализирует новый экземпляр <see cref="Protocol" />.
	/// </summary>
	/// <param name="id">Идентификатор.</param>
	/// <param name="name">Наименование.</param>
	public Protocol(Guid id, string name)
		: base(id) =>
		Name = name;
	#endregion

	#region Properties
	/// <summary>
	/// Возвращает наименование протокола.
	/// </summary>
	public string Name
	{
		get;
		init;
	}
	#endregion
}
