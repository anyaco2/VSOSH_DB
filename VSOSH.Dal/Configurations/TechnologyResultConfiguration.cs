using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal.Configurations;

/// <summary>
/// Представляет конфигурацию для <see cref="TechnologyResult" />.
/// </summary>
public class TechnologyResultConfiguration : IEntityTypeConfiguration<TechnologyResult>
{
	#region IEntityTypeConfiguration<TechnologyResult> members
	public void Configure(EntityTypeBuilder<TechnologyResult> builder)
	{
		builder.ToTable("TechnologyResult");

		builder.Property(t => t.DirectionPractice)
			   .HasColumnName("DirectionPractice");
	}
	#endregion
}
