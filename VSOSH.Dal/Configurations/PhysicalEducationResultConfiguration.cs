using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VSOSH.Domain;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal.Configurations;

/// <summary>
/// Представляет конфигурацию для <see cref="PhysicalEducationResult" />.
/// </summary>
public class PhysicalEducationResultConfiguration : IEntityTypeConfiguration<PhysicalEducationResult>
{
    public void Configure(EntityTypeBuilder<PhysicalEducationResult> builder)
    {
        builder.ToTable("PhysicalEducationResult");

        builder.Property(p => p.FinalScoreInPractice)
            .HasColumnName("FinalScoreInPractice");
        builder.Property(p => p.PreliminaryScoreInPractice)
            .HasColumnName("PreliminaryScoreInPractice");
        builder.Property(p => p.FinalScoreInTheory)
            .HasColumnName("FinalScoreInTheory");
        builder.Property(p => p.PreliminaryScoreInTheory)
            .HasColumnName("PreliminaryScoreInTheory");
        builder.Property(p => p.Sex)
            .HasConversion<EnumToStringConverter<Sex>>()
            .HasColumnName("Sex");
    }
}