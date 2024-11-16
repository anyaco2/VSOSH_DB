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
                name: "ArtResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronomyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronomyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiologyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiologyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChemistryResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistryResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChineseResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChineseResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerScienceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerScienceResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcologyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcologyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EconomyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnglishResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrenchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrenchResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundamentalsLifeSafetyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundamentalsLifeSafetyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeographyResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeographyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GermanResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GermanResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LawResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiteratureResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteratureResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MathResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MathResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicResults", x => x.Id);
                });

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
                name: "RussianResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RussianResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RussianResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialStudiesResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialStudiesResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialStudiesResults_SchoolOlympiadResultBase_Id",
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
                name: "ArtResults");

            migrationBuilder.DropTable(
                name: "AstronomyResults");

            migrationBuilder.DropTable(
                name: "BiologyResults");

            migrationBuilder.DropTable(
                name: "ChemistryResults");

            migrationBuilder.DropTable(
                name: "ChineseResults");

            migrationBuilder.DropTable(
                name: "ComputerScienceResults");

            migrationBuilder.DropTable(
                name: "EcologyResults");

            migrationBuilder.DropTable(
                name: "EconomyResults");

            migrationBuilder.DropTable(
                name: "EnglishResults");

            migrationBuilder.DropTable(
                name: "FrenchResults");

            migrationBuilder.DropTable(
                name: "FundamentalsLifeSafetyResults");

            migrationBuilder.DropTable(
                name: "GeographyResults");

            migrationBuilder.DropTable(
                name: "GermanResults");

            migrationBuilder.DropTable(
                name: "HistoryResults");

            migrationBuilder.DropTable(
                name: "LawResults");

            migrationBuilder.DropTable(
                name: "LiteratureResults");

            migrationBuilder.DropTable(
                name: "MathResults");

            migrationBuilder.DropTable(
                name: "PhysicalEducationResult");

            migrationBuilder.DropTable(
                name: "PhysicResults");

            migrationBuilder.DropTable(
                name: "RussianResults");

            migrationBuilder.DropTable(
                name: "SocialStudiesResults");

            migrationBuilder.DropTable(
                name: "TechnologyResult");

            migrationBuilder.DropTable(
                name: "SchoolOlympiadResultBase");
        }
    }
}
