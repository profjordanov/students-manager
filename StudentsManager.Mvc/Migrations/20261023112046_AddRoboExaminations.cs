using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddRoboExaminations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoboExaminations",
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
                    table.PrimaryKey("PK_RoboExaminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoboExaminationAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoboExaminationId = table.Column<int>(type: "int", nullable: false),
                    ExaminationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnswersJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EvaluationStartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RawEvaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoboExaminationAttempts", x => x.Id);
                    table.CheckConstraint("CK_RoboExaminationAttempts_Grade", "[Grade] IS NULL OR [Grade] BETWEEN 2 AND 6");
                    table.ForeignKey(
                        name: "FK_RoboExaminationAttempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoboExaminationAttempts_RoboExaminations_RoboExaminationId",
                        column: x => x.RoboExaminationId,
                        principalTable: "RoboExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoboExaminationAttempts_RoboExaminationId",
                table: "RoboExaminationAttempts",
                column: "RoboExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoboExaminationAttempts_UserId_ExaminationType",
                table: "RoboExaminationAttempts",
                columns: new[] { "UserId", "ExaminationType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoboExaminations_Type_Variant",
                table: "RoboExaminations",
                columns: new[] { "Type", "Variant" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoboExaminationAttempts");

            migrationBuilder.DropTable(
                name: "RoboExaminations");
        }
    }
}
