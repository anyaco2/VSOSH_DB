namespace VSOSH.Domain.Services;

public interface IGreaterClassService
{
	#region Overridable
	Task<FileStream> GetGreaterClass(CancellationToken cancellationToken = default);
	#endregion
}
