using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Migrations
{
    /// <inheritdoc />
    public partial class projectassignToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectAssigns",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssigns", x => new { x.ProjectId, x.UserEmployeeId });
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    projectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dueDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    turntime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instruction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAssigns");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
