using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingApp.Migrations.AdminDb
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reviewer",
                table: "TvShow",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviewer",
                table: "TvShow");
        }
    }
}
