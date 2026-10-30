using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddRoboQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoboQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Variant = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StringifiedJsonContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoboQuizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoboQuizUserResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScorePercentage = table.Column<int>(type: "int", nullable: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoboQuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoboQuizUserResults", x => x.Id);
                    table.CheckConstraint("CK_RoboQuizUserResults_ScorePercentage", "[ScorePercentage] BETWEEN 0 AND 100");
                    table.ForeignKey(
                        name: "FK_RoboQuizUserResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoboQuizUserResults_RoboQuizzes_RoboQuizId",
                        column: x => x.RoboQuizId,
                        principalTable: "RoboQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoboQuizUserResults_RoboQuizId",
                table: "RoboQuizUserResults",
                column: "RoboQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_RoboQuizUserResults_UserId_RoboQuizId",
                table: "RoboQuizUserResults",
                columns: new[] { "UserId", "RoboQuizId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoboQuizzes_Type_Variant",
                table: "RoboQuizzes",
                columns: new[] { "Type", "Variant" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoboQuizUserResults");

            migrationBuilder.DropTable(
                name: "RoboQuizzes");
        }
    }
}
