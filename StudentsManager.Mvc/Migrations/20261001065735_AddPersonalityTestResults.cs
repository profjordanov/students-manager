using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalityTestResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalityTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionScore = table.Column<int>(type: "int", nullable: false, comment: "Number of chosen Action statements"),
                    ProcessScore = table.Column<int>(type: "int", nullable: false, comment: "Number of chosen Process statements"),
                    PeopleScore = table.Column<int>(type: "int", nullable: false, comment: "Number of chosen People statements"),
                    IdeaScore = table.Column<int>(type: "int", nullable: false, comment: "Number of chosen Idea statements"),
                    ActionPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "Action score as a percentage of the maximum Action score"),
                    ProcessPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "Process score as a percentage of the maximum Process score"),
                    PeoplePercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "People score as a percentage of the maximum People score"),
                    IdeaPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "Idea score as a percentage of the maximum Idea score"),
                    CompletedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, comment: "When the user completed the test"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalityTestResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityTestResults_UserId",
                table: "PersonalityTestResults",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalityTestResults");
        }
    }
}
