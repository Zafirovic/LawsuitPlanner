using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class Finalize2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Lawsuits_lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lawsuitid",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "LawsuitLawyer",
                columns: table => new
                {
                    lawsuitId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    userId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawsuitLawyer", x => new { x.userId, x.lawsuitId });
                    table.ForeignKey(
                        name: "FK_LawsuitLawyer_Lawsuits_lawsuitId",
                        column: x => x.lawsuitId,
                        principalTable: "Lawsuits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawsuitLawyer_AspNetUsers_userId1",
                        column: x => x.userId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LawsuitLawyer_lawsuitId",
                table: "LawsuitLawyer",
                column: "lawsuitId");

            migrationBuilder.CreateIndex(
                name: "IX_LawsuitLawyer_userId1",
                table: "LawsuitLawyer",
                column: "userId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LawsuitLawyer");

            migrationBuilder.AddColumn<int>(
                name: "lawsuitid",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_lawsuitid",
                table: "AspNetUsers",
                column: "lawsuitid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Lawsuits_lawsuitid",
                table: "AspNetUsers",
                column: "lawsuitid",
                principalTable: "Lawsuits",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
