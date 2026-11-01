using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AllowZeroBasedRoboExaminationGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RoboExaminationAttempts_Grade",
                table: "RoboExaminationAttempts");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoboExaminationAttempts_Grade",
                table: "RoboExaminationAttempts",
                sql: "[Grade] IS NULL OR [Grade] BETWEEN 0 AND 6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RoboExaminationAttempts_Grade",
                table: "RoboExaminationAttempts");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoboExaminationAttempts_Grade",
                table: "RoboExaminationAttempts",
                sql: "[Grade] IS NULL OR [Grade] BETWEEN 2 AND 6");
        }
    }
}
