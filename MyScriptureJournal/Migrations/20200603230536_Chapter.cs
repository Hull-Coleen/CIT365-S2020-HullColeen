using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Scripture_Journal.Migrations
{
    public partial class Chapter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "JournalEntry");

            migrationBuilder.AddColumn<string>(
                name: "Chapter",
                table: "JournalEntry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chapter",
                table: "JournalEntry");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "JournalEntry",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
