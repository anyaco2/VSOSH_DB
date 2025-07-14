using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSOSH.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddProtocolTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolId",
                table: "SchoolOlympiadResultBase",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Protocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocols", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolOlympiadResultBase_ProtocolId",
                table: "SchoolOlympiadResultBase",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolOlympiadResultBase_Protocols_ProtocolId",
                table: "SchoolOlympiadResultBase",
                column: "ProtocolId",
                principalTable: "Protocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolOlympiadResultBase_Protocols_ProtocolId",
                table: "SchoolOlympiadResultBase");

            migrationBuilder.DropTable(
                name: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_SchoolOlympiadResultBase_ProtocolId",
                table: "SchoolOlympiadResultBase");

            migrationBuilder.DropColumn(
                name: "ProtocolId",
                table: "SchoolOlympiadResultBase");
        }
    }
}
