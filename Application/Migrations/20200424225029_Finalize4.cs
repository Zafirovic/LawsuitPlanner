using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Finalize4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId1",
                table: "LawsuitLawyer");

            migrationBuilder.DropIndex(
                name: "IX_LawsuitLawyer_userId1",
                table: "LawsuitLawyer");

            migrationBuilder.DropColumn(
                name: "userId1",
                table: "LawsuitLawyer");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "LawsuitLawyer",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId",
                table: "LawsuitLawyer",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId",
                table: "LawsuitLawyer");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "LawsuitLawyer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "userId1",
                table: "LawsuitLawyer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LawsuitLawyer_userId1",
                table: "LawsuitLawyer",
                column: "userId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LawsuitLawyer_AspNetUsers_userId1",
                table: "LawsuitLawyer",
                column: "userId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
