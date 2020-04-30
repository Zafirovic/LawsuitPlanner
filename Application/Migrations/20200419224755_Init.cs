using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parnice_Parnicaid",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Parnice");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "Kontakti");

            migrationBuilder.DropTable(
                name: "TipPostupaka");

            migrationBuilder.DropTable(
                name: "Kompanije");

            migrationBuilder.RenameColumn(
                name: "prezime",
                table: "AspNetUsers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "ime",
                table: "AspNetUsers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Parnicaid",
                table: "AspNetUsers",
                newName: "Lawsuitid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Parnicaid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Lawsuitid");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(40)", nullable: true),
                    address = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cityName = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfProcesses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfProcesses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    phone1 = table.Column<string>(type: "varchar(10)", nullable: true),
                    phone2 = table.Column<string>(type: "varchar(10)", nullable: true),
                    address = table.Column<string>(type: "varchar(40)", nullable: true),
                    email = table.Column<string>(type: "varchar(30)", nullable: true),
                    legalPerson = table.Column<bool>(nullable: false),
                    job = table.Column<string>(type: "varchar(30)", nullable: true),
                    companyid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_companyid",
                        column: x => x.companyid,
                        principalTable: "Companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lawsuits",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dateTimeOfEvent = table.Column<DateTime>(nullable: false),
                    locationid = table.Column<int>(nullable: true),
                    judgeid = table.Column<int>(nullable: true),
                    courtType = table.Column<int>(nullable: false),
                    processId = table.Column<string>(type: "varchar(15)", nullable: false),
                    courtroomNumber = table.Column<string>(type: "varchar(5)", nullable: false),
                    prosecutorid = table.Column<int>(nullable: true),
                    defendantid = table.Column<int>(nullable: true),
                    note = table.Column<string>(type: "varchar(30)", nullable: false),
                    typeOfProcessid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawsuits", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lawsuits_Contacts_defendantid",
                        column: x => x.defendantid,
                        principalTable: "Contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lawsuits_Contacts_judgeid",
                        column: x => x.judgeid,
                        principalTable: "Contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lawsuits_Locations_locationid",
                        column: x => x.locationid,
                        principalTable: "Locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lawsuits_Contacts_prosecutorid",
                        column: x => x.prosecutorid,
                        principalTable: "Contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lawsuits_TypeOfProcesses_typeOfProcessid",
                        column: x => x.typeOfProcessid,
                        principalTable: "TypeOfProcesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_companyid",
                table: "Contacts",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "IX_Lawsuits_defendantid",
                table: "Lawsuits",
                column: "defendantid");

            migrationBuilder.CreateIndex(
                name: "IX_Lawsuits_judgeid",
                table: "Lawsuits",
                column: "judgeid");

            migrationBuilder.CreateIndex(
                name: "IX_Lawsuits_locationid",
                table: "Lawsuits",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Lawsuits_prosecutorid",
                table: "Lawsuits",
                column: "prosecutorid");

            migrationBuilder.CreateIndex(
                name: "IX_Lawsuits_typeOfProcessid",
                table: "Lawsuits",
                column: "typeOfProcessid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lawsuits_Lawsuitid",
                table: "AspNetUsers",
                column: "Lawsuitid",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lawsuits_Lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lawsuits");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TypeOfProcesses");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "AspNetUsers",
                newName: "prezime");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUsers",
                newName: "ime");

            migrationBuilder.RenameColumn(
                name: "Lawsuitid",
                table: "AspNetUsers",
                newName: "Parnicaid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Lawsuitid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Parnicaid");

            migrationBuilder.CreateTable(
                name: "Kompanije",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    adresa = table.Column<string>(type: "varchar(40)", nullable: true),
                    naziv = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompanije", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "Kontakti",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    adresa = table.Column<string>(type: "varchar(40)", nullable: true),
                    email = table.Column<string>(type: "varchar(30)", nullable: true),
                    ime = table.Column<string>(type: "varchar(50)", nullable: false),
                    kompanijaid = table.Column<int>(nullable: true),
                    pravnoLice = table.Column<bool>(nullable: false),
                    telefon1 = table.Column<string>(type: "varchar(10)", nullable: true),
                    telefon2 = table.Column<string>(type: "varchar(10)", nullable: true),
                    zanimanje = table.Column<string>(type: "varchar(30)", nullable: true)
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
                name: "Parnice",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    brojSudnice = table.Column<string>(type: "varchar(5)", nullable: false),
                    datumVremeOdrzavanja = table.Column<DateTime>(nullable: false),
                    idPostupka = table.Column<string>(type: "varchar(15)", nullable: false),
                    lokacijaid = table.Column<int>(nullable: true),
                    napomena = table.Column<string>(type: "varchar(30)", nullable: false),
                    sudijaid = table.Column<int>(nullable: true),
                    tipPostupkaid = table.Column<int>(nullable: true),
                    tipSuda = table.Column<int>(nullable: false),
                    tuzenikid = table.Column<int>(nullable: true),
                    tuzilacid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parnice", x => x.id);
                    table.ForeignKey(
                        name: "FK_Parnice_Lokacije_lokacijaid",
                        column: x => x.lokacijaid,
                        principalTable: "Lokacije",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parnice_Kontakti_sudijaid",
                        column: x => x.sudijaid,
                        principalTable: "Kontakti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parnice_TipPostupaka_tipPostupkaid",
                        column: x => x.tipPostupkaid,
                        principalTable: "TipPostupaka",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parnice_Kontakti_tuzenikid",
                        column: x => x.tuzenikid,
                        principalTable: "Kontakti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parnice_Kontakti_tuzilacid",
                        column: x => x.tuzilacid,
                        principalTable: "Kontakti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kontakti_kompanijaid",
                table: "Kontakti",
                column: "kompanijaid");

            migrationBuilder.CreateIndex(
                name: "IX_Parnice_lokacijaid",
                table: "Parnice",
                column: "lokacijaid");

            migrationBuilder.CreateIndex(
                name: "IX_Parnice_sudijaid",
                table: "Parnice",
                column: "sudijaid");

            migrationBuilder.CreateIndex(
                name: "IX_Parnice_tipPostupkaid",
                table: "Parnice",
                column: "tipPostupkaid");

            migrationBuilder.CreateIndex(
                name: "IX_Parnice_tuzenikid",
                table: "Parnice",
                column: "tuzenikid");

            migrationBuilder.CreateIndex(
                name: "IX_Parnice_tuzilacid",
                table: "Parnice",
                column: "tuzilacid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parnice_Parnicaid",
                table: "AspNetUsers",
                column: "Parnicaid",
                principalTable: "Parnice",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
