using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolOlympiadResultBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    School = table.Column<string>(type: "text", nullable: false),
                    ParticipantCode = table.Column<string>(type: "text", nullable: false),
                    CurrentGrade = table.Column<int>(type: "integer", nullable: true),
                    GradeCompeting = table.Column<int>(type: "integer", nullable: false),
                    FinalScore = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<double>(type: "double precision", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolOlympiadResultBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalEducationResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PreliminaryScoreInTheory = table.Column<double>(type: "double precision", nullable: false),
                    FinalScoreInTheory = table.Column<double>(type: "double precision", nullable: false),
                    PreliminaryScoreInPractice = table.Column<double>(type: "double precision", nullable: false),
                    FinalScoreInPractice = table.Column<double>(type: "double precision", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalEducationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalEducationResult_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnologyResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectionPractice = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologyResult_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysicalEducationResult");

            migrationBuilder.DropTable(
                name: "TechnologyResult");

            migrationBuilder.DropTable(
                name: "SchoolOlympiadResultBase");
        }
    }
}
