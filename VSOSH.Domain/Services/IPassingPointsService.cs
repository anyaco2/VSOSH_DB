namespace VSOSH.Domain.Services;

public interface IPassingPointsService
{
    Task<FileStream> GetPassingPoints(Subject subject, CancellationToken cancellationToken = default);
}