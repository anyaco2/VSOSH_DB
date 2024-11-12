using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VSOSH.Domain;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal.Configurations;

/// <summary>
/// Представляет конфигурацию для <see cref="SchoolOlympiadResultBase" />.
/// </summary>
public class SchoolOlympiadResultBaseConfiguration : IEntityTypeConfiguration<SchoolOlympiadResultBase>
{
    public void Configure(EntityTypeBuilder<SchoolOlympiadResultBase> builder)
    {
        builder.ToTable("SchoolOlympiadResultBase");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(s => s.StudentName, propertyBuilder =>
        {
            propertyBuilder.Property(s => s.FirstName)
                .HasColumnName("FirstName");
            propertyBuilder.Property(s => s.LastName)
                .HasColumnName("LastName");
            propertyBuilder.Property(s => s.MiddleName)
                .HasColumnName("MiddleName");
        });

        builder.Property(s => s.Percentage)
            .HasColumnName("Percentage");
        builder.Property(s => s.Status)
            .HasConversion<EnumToStringConverter<Status>>()
            .HasColumnName("Status");
        builder.Property(s => s.GradeCompeting)
            .HasColumnName("GradeCompeting");
        builder.Property(s => s.CurrentGrade)
            .HasColumnName("CurrentGrade");
        builder.Property(s => s.School)
            .HasColumnName("School");
        builder.Property(s => s.ParticipantCode)
            .HasColumnName("ParticipantCode");
    }
}