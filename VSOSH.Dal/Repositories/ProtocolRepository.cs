using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;

namespace VSOSH.Dal.Repositories;

/// <summary>
/// Представляет реализацию <see cref="IProtocolRepository" />.
/// </summary>
/// <param name="dbContext"><see cref="ApplicationDbContext" />.</param>
public class ProtocolRepository(ApplicationDbContext dbContext) : IProtocolRepository
{
	#region Data
	#region Fields
	private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	#endregion
	#endregion

	#region IProtocolRepository members
	/// <inheritdoc />
	public void Add(Protocol protocol)
	{
		_dbContext.Set<Protocol>()
				  .Add(protocol);
	}

	public Protocol[] FindAll() =>
		_dbContext.Set<Protocol>()
				  .ToArray();

	/// <inheritdoc />
	public Task<Protocol?> FindById(Guid id) => throw new NotImplementedException();

	/// <inheritdoc />
	public void Remove(Protocol protocol)
	{
		_dbContext.Set<Protocol>()
				  .Remove(protocol);
		_dbContext.SaveChanges();
	}
	#endregion
}
