using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Finalize3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LawsuitLawyer",
                table: "LawsuitLawyer");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                table: "LawsuitLawyer",
                columns: new[] { "userId", "lawsuitId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id",
                table: "LawsuitLawyer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LawsuitLawyer",
                table: "LawsuitLawyer",
                columns: new[] { "userId", "lawsuitId" });
        }
    }
}
