using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKR_Visik.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    sections_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sections_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sections_Data = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.sections_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    users_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_FIO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    users_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    users_Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Users_AccountActive = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.users_Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    themes_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    themes_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    themes_data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sectionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.themes_id);
                    table.ForeignKey(
                        name: "FK_Themes_Sections_sectionsId",
                        column: x => x.sectionsId,
                        principalTable: "Sections",
                        principalColumn: "sections_Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageHistory",
                columns: table => new
                {
                    MH_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MH_who = table.Column<int>(type: "int", nullable: true),
                    MH_theme = table.Column<int>(type: "int", nullable: true),
                    MH_placemant = table.Column<int>(type: "int", nullable: true),
                    MH_answer = table.Column<int>(type: "int", nullable: true),
                    MH_data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MH_TheMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageHistory", x => x.MH_id);
                    table.ForeignKey(
                        name: "FK_MessageHistory_Themes_MH_theme",
                        column: x => x.MH_theme,
                        principalTable: "Themes",
                        principalColumn: "themes_id");
                    table.ForeignKey(
                        name: "FK_MessageHistory_Users_MH_who",
                        column: x => x.MH_who,
                        principalTable: "Users",
                        principalColumn: "users_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageHistory_MH_theme",
                table: "MessageHistory",
                column: "MH_theme");

            migrationBuilder.CreateIndex(
                name: "IX_MessageHistory_MH_who",
                table: "MessageHistory",
                column: "MH_who");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_sectionsId",
                table: "Themes",
                column: "sectionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageHistory");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
