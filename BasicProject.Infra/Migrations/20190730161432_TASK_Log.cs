using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicProject.Infra.Migrations
{
    public partial class TASK_Log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    FriendlyMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
