using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddViewGeneralReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW GeneralReportView AS
            SELECT 
                COUNT(*) AS TotalCount, 
                COUNT(DISTINCT CONCAT(""LastName"", ' ', ""FirstName"", ' ', ""MiddleName"")) AS UniqueParticipants, 
                COUNT(*) FILTER (WHERE ""Status"" = 'Winner') AS TotalWinnerDiplomas, 
                COUNT(DISTINCT CONCAT(""LastName"", ' ', ""FirstName"", ' ', ""MiddleName"")) FILTER (WHERE ""Status"" = 'Winner') AS UniqueWinners, 
                COUNT(*) FILTER (WHERE ""Status"" = 'Awardee') AS TotalPrizeDiplomas, 
                COUNT(DISTINCT CONCAT(""LastName"", ' ', ""FirstName"", ' ', ""MiddleName"")) FILTER (WHERE ""Status"" = 'Awardee') AS UniquePrizeWinners, 
                COUNT(DISTINCT CONCAT(""LastName"", ' ', ""FirstName"", ' ', ""MiddleName"")) FILTER (WHERE ""Status"" IN ('Winner', 'Awardee')) AS UniqueWinnersAndPrizeWinners
            FROM ""SchoolOlympiadResultBase"";
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS GeneralReportView;");
        }
    }
}
