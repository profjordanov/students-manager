using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddRoboQuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoboQuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoboQuizId = table.Column<int>(type: "int", nullable: false),
                    ExaminationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoboQuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoboQuizAttempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoboQuizAttempts_RoboQuizzes_RoboQuizId",
                        column: x => x.RoboQuizId,
                        principalTable: "RoboQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoboQuizAttempts_RoboQuizId",
                table: "RoboQuizAttempts",
                column: "RoboQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_RoboQuizAttempts_UserId_ExaminationType",
                table: "RoboQuizAttempts",
                columns: new[] { "UserId", "ExaminationType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoboQuizAttempts");
        }
    }
}
