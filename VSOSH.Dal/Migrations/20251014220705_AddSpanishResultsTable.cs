using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddSpanishResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpanishResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpanishResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpanishResults_SchoolOlympiadResultBase_Id",
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
                name: "SpanishResults");
        }
    }
}
