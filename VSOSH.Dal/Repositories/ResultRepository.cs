using VSOSH.Domain.Entities;
using VSOSH.Domain.Repositories;

namespace VSOSH.Dal.Repositories;

/// <summary>
/// Представляет реализацию <see cref="IResultRepository" />.
/// </summary>
/// <param name="dbContext"><see cref="ApplicationDbContext" />.</param>
public class ResultRepository(ApplicationDbContext dbContext) : IResultRepository
{
    #region Data

    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    #endregion

    public async Task AddRangeAsync(IReadOnlyCollection<SchoolOlympiadResultBase> resultBases,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.AddRangeAsync(resultBases, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}