using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Finalize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lawsuits_Lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Lawsuitid",
                table: "AspNetUsers",
                newName: "lawsuitid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Lawsuitid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_lawsuitid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lawsuits_lawsuitid",
                table: "AspNetUsers",
                column: "lawsuitid",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lawsuits_lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "lawsuitid",
                table: "AspNetUsers",
                newName: "Lawsuitid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_lawsuitid",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Lawsuitid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lawsuits_Lawsuitid",
                table: "AspNetUsers",
                column: "Lawsuitid",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
