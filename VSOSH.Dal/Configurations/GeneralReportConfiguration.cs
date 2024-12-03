using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VSOSH.Domain.Entities;

namespace VSOSH.Dal.Configurations;

public class GeneralReportConfiguration : IEntityTypeConfiguration<GeneralReport>
{
    public void Configure(EntityTypeBuilder<GeneralReport> builder)
    {
        builder.ToView("generalreportview");

        builder.HasNoKey();

        builder.Property(g => g.TotalCount)
            .HasColumnName("totalcount");

        builder.Property(g => g.UniqueWinners)
            .HasColumnName("uniquewinners");

        builder.Property(g => g.UniqueParticipants)
            .HasColumnName("uniqueparticipants");

        builder.Property(g => g.UniquePrizeWinners)
            .HasColumnName("uniqueprizewinners");

        builder.Property(g => g.TotalWinnerDiplomas)
            .HasColumnName("totalwinnerdiplomas");

        builder.Property(g => g.TotalPrizeDiplomas)
            .HasColumnName("totalprizediplomas");

        builder.Property(g => g.UniqueWinnersAndPrizeWinners)
            .HasColumnName("uniquewinnersandprizewinners");
    }
}