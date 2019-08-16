using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicProject.Infra.Migrations
{
    public partial class TASK_Address_And_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
