using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal.Configurations;

public class ProtocolConfiguration : IEntityTypeConfiguration<Protocol>
{
	#region IEntityTypeConfiguration<Protocol> members
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<Protocol> builder)
	{
		builder.ToTable("Protocols");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Name);
	}
	#endregion
}
