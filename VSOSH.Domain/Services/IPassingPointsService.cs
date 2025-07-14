namespace VSOSH.Domain.Services;

public interface IPassingPointsService
{
	#region Overridable
	Task<FileStream> GetPassingPoints(Subject subject, CancellationToken cancellationToken = default);
	#endregion
}
