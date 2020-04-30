using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class PrvaVerzija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kompanija",
                table: "Kompanija");

            migrationBuilder.RenameTable(
                name: "Kompanija",
                newName: "Kompanije");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kompanije",
                table: "Kompanije",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Kontakti",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ime = table.Column<string>(type: "varchar(50)", nullable: false),
                    telefon1 = table.Column<string>(type: "varchar(10)", nullable: true),
                    telefon2 = table.Column<string>(type: "varchar(10)", nullable: true),
                    adresa = table.Column<string>(type: "varchar(40)", nullable: true),
                    email = table.Column<string>(type: "varchar(30)", nullable: true),
                    pravnoLice = table.Column<bool>(nullable: false),
                    zanimanje = table.Column<string>(type: "varchar(30)", nullable: true),
                    kompanijaid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakti", x => x.id);
                    table.ForeignKey(
                        name: "FK_Kontakti_Kompanije_kompanijaid",
                        column: x => x.kompanijaid,
                        principalTable: "Kompanije",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Korisici",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ime = table.Column<string>(type: "varchar(40)", nullable: false),
                    admin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisici", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naslov = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipPostupaka",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    naslov = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPostupaka", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kontakti_kompanijaid",
                table: "Kontakti",
                column: "kompanijaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kontakti");

            migrationBuilder.DropTable(
                name: "Korisici");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "TipPostupaka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kompanije",
                table: "Kompanije");

            migrationBuilder.RenameTable(
                name: "Kompanije",
                newName: "Kompanija");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kompanija",
                table: "Kompanija",
                column: "id");
        }
    }
}
