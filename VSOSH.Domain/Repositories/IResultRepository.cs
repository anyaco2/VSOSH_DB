using VSOSH.Domain.Entities;

namespace VSOSH.Domain.Repositories;

/// <summary>
/// Представляет интерфейс репозитория результата по олимпиаде.
/// </summary>
public interface IResultRepository
{
    /// <summary>
    /// Добавляет результаты в базу данных.
    /// </summary>
    /// <param name="resultBases">Результаты.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Результат задачи по добавлению результатов по олимпиаде.</returns>
    Task AddRangeAsync(IReadOnlyCollection<SchoolOlympiadResultBase> resultBases,
        CancellationToken cancellationToken = default);
}