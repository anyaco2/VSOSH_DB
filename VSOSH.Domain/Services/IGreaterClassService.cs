namespace VSOSH.Domain.Services;

public interface IGreaterClassService
{
    Task<FileStream> GetGreaterClass(CancellationToken cancellationToken = default);
}