using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companies_companyid",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_defendantid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_judgeid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Locations_locationid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_prosecutorid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_TypeOfProcesses_typeOfProcessid",
                table: "Lawsuits");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companies_companyid",
                table: "Contacts",
                column: "companyid",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_defendantid",
                table: "Lawsuits",
                column: "defendantid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_judgeid",
                table: "Lawsuits",
                column: "judgeid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Locations_locationid",
                table: "Lawsuits",
                column: "locationid",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_prosecutorid",
                table: "Lawsuits",
                column: "prosecutorid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_TypeOfProcesses_typeOfProcessid",
                table: "Lawsuits",
                column: "typeOfProcessid",
                principalTable: "TypeOfProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companies_companyid",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_defendantid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_judgeid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Locations_locationid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_Contacts_prosecutorid",
                table: "Lawsuits");

            migrationBuilder.DropForeignKey(
                name: "FK_Lawsuits_TypeOfProcesses_typeOfProcessid",
                table: "Lawsuits");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companies_companyid",
                table: "Contacts",
                column: "companyid",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_defendantid",
                table: "Lawsuits",
                column: "defendantid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_judgeid",
                table: "Lawsuits",
                column: "judgeid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Locations_locationid",
                table: "Lawsuits",
                column: "locationid",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_Contacts_prosecutorid",
                table: "Lawsuits",
                column: "prosecutorid",
                principalTable: "Contacts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lawsuits_TypeOfProcesses_typeOfProcessid",
                table: "Lawsuits",
                column: "typeOfProcessid",
                principalTable: "TypeOfProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
