using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Migrations
{
    /// <inheritdoc />
    public partial class projectAttachFile2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "ProjectAttachFiles",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attachFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAttachFiles", x => new { x.ProjectId, x.attachFileId });
                });

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "ProjectAttachFiles");

            
        }
    }
}
