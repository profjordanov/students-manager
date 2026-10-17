using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalityTestFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeedbackComment",
                table: "PersonalityTestResults",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Optional free-text feedback about the result");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackRating",
                table: "PersonalityTestResults",
                type: "int",
                nullable: true,
                comment: "How accurately the result matches the user, from 1 to 5");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FeedbackSubmittedAtUtc",
                table: "PersonalityTestResults",
                type: "datetimeoffset",
                nullable: true,
                comment: "When the user last submitted feedback");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackComment",
                table: "PersonalityTestResults");

            migrationBuilder.DropColumn(
                name: "FeedbackRating",
                table: "PersonalityTestResults");

            migrationBuilder.DropColumn(
                name: "FeedbackSubmittedAtUtc",
                table: "PersonalityTestResults");
        }
    }
}
