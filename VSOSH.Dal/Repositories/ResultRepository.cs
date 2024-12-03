using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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

    /// <inheritdoc />
    public async Task AddRangeAsync(IReadOnlyCollection<SchoolOlympiadResultBase> resultBases,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.AddRangeAsync(resultBases, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<T>?> FindRangeAsync<T>(Expression<Func<T, bool>>? findExpression = null,
        CancellationToken cancellationToken = default) where T : SchoolOlympiadResultBase
    {
        if (findExpression is null)
        {
            return await _dbContext
                .SchoolOlympiadResultBases
                .AsNoTrackingWithIdentityResolution()
                .OfType<T>()
                .OrderBy(r => r.GradeCompeting)
                .ToListAsync(cancellationToken);
        }

        return await _dbContext
            .SchoolOlympiadResultBases
            .AsNoTrackingWithIdentityResolution()
            .OfType<T>()
            .Where(findExpression)
            .OrderBy(r => r.GradeCompeting)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<GeneralReport?> GetGeneralReport(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<GeneralReport>()
            .FirstOrDefaultAsync(cancellationToken);
    }
}