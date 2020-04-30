using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyer_Lawsuits_lawsuitId",
                table: "LawsuitLawyer");

            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId",
                table: "LawsuitLawyer");

            migrationBuilder.DropPrimaryKey(
                name: "id",
                table: "LawsuitLawyer");

            migrationBuilder.RenameTable(
                name: "LawsuitLawyer",
                newName: "LawsuitLawyers");

            migrationBuilder.RenameIndex(
                name: "IX_LawsuitLawyer_lawsuitId",
                table: "LawsuitLawyers",
                newName: "IX_LawsuitLawyers_lawsuitId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "LawsuitLawyers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "LawsuitLawyers",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LawsuitLawyers",
                table: "LawsuitLawyers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_LawsuitLawyers_userId",
                table: "LawsuitLawyers",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyers_Lawsuits_lawsuitId",
                table: "LawsuitLawyers",
                column: "lawsuitId",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyers_Lawsuits_lawsuitId",
                table: "LawsuitLawyers");

            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyers_AspNetUsers_userId",
                table: "LawsuitLawyers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LawsuitLawyers",
                table: "LawsuitLawyers");

            migrationBuilder.DropIndex(
                name: "IX_LawsuitLawyers_userId",
                table: "LawsuitLawyers");

            migrationBuilder.DropColumn(
                name: "id",
                table: "LawsuitLawyers");

            migrationBuilder.RenameTable(
                name: "LawsuitLawyers",
                newName: "LawsuitLawyer");

            migrationBuilder.RenameIndex(
                name: "IX_LawsuitLawyers_lawsuitId",
                table: "LawsuitLawyer",
                newName: "IX_LawsuitLawyer_lawsuitId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "LawsuitLawyer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "id",
                table: "LawsuitLawyer",
                columns: new[] { "userId", "lawsuitId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyer_Lawsuits_lawsuitId",
                table: "LawsuitLawyer",
                column: "lawsuitId",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId",
                table: "LawsuitLawyer",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
