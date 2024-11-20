using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddAllSubjects : Migration
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
                    table.ForeignKey(
                        name: "FK_ArtResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_AstronomyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_BiologyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_ChemistryResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_ChineseResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_ComputerScienceResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_EcologyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_EconomyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_EnglishResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_FrenchResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_FundamentalsLifeSafetyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_GeographyResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_GermanResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_HistoryResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_LawResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_LiteratureResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_MathResults_SchoolOlympiadResultBase_Id",
                        column: x => x.Id,
                        principalTable: "SchoolOlympiadResultBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_PhysicResults_SchoolOlympiadResultBase_Id",
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
                name: "PhysicResults");

            migrationBuilder.DropTable(
                name: "RussianResults");

            migrationBuilder.DropTable(
                name: "SocialStudiesResults");
        }
    }
}
