using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Knigi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avtor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nacionalnost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DatumRagjanje = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kniga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AvtorId = table.Column<int>(type: "int", nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kniga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kniga_Avtor_AvtorId",
                        column: x => x.AvtorId,
                        principalTable: "Avtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnigaZanr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnigaId = table.Column<int>(type: "int", nullable: false),
                    ZanrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnigaZanr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnigaZanr_Kniga_KnigaId",
                        column: x => x.KnigaId,
                        principalTable: "Kniga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KnigaZanr_Zanr_ZanrId",
                        column: x => x.ZanrId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kniga_AvtorId",
                table: "Kniga",
                column: "AvtorId");

            migrationBuilder.CreateIndex(
                name: "IX_KnigaZanr_KnigaId",
                table: "KnigaZanr",
                column: "KnigaId");

            migrationBuilder.CreateIndex(
                name: "IX_KnigaZanr_ZanrId",
                table: "KnigaZanr",
                column: "ZanrId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnigaZanr");

            migrationBuilder.DropTable(
                name: "Kniga");

            migrationBuilder.DropTable(
                name: "Zanr");

            migrationBuilder.DropTable(
                name: "Avtor");
        }
    }
}
