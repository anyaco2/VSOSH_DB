using VSOSH.Domain.Entities;

namespace VSOSH.Domain.Repositories;

public interface IProtocolRepository
{
	#region Overridable
	/// <summary>
	/// Добавляет протокол.
	/// </summary>
	/// <param name="protocol">Протокол.</param>
	void Add(Protocol protocol);

	Protocol[] FindAll();

	/// <summary>
	/// Возвращает протокол.
	/// </summary>
	/// <param name="id">Идентификатор протокола.</param>
	/// <returns>Протокол.</returns>
	Task<Protocol?> FindById(Guid id);

	/// <summary>
	/// Удаляет протокол.
	/// </summary>
	/// <param name="protocol">Протокол.</param>
	void Remove(Protocol protocol);
	#endregion
}
