using System.Linq.Expressions;
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

    /// <summary>
    /// Возвращает коллекцию результатов по условию.
    /// </summary>
    /// <param name="findExpression">Условие.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <typeparam name="T"><see cref="SchoolOlympiadResultBase" />.</typeparam>
    /// <returns>Коллекция результатов по условию.</returns>
    Task<IReadOnlyCollection<T>?> FindRangeAsync<T>(Expression<Func<T, bool>>? findExpression = null,
        CancellationToken cancellationToken = default) where T : SchoolOlympiadResultBase;

    /// <summary>
    /// Возвращает общий отчет.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Общий отчет.</returns>
    Task<GeneralReport?> GetGeneralReport(CancellationToken cancellationToken = default);
}