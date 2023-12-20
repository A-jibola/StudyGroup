using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyGroup.Migrations
{
    public partial class AddGroupInfoToReminderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "Reminders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Reminders");
        }
    }
}
