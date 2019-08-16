using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicProject.Infra.Migrations
{
    public partial class TASK_Log_Add_UserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Log",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Log");
        }
    }
}
