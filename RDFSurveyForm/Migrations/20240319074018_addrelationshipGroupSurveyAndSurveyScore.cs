using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class addrelationshipGroupSurveyAndSurveyScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyScoreId",
                table: "GroupSurvey",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupSurvey_SurveyScoreId",
                table: "GroupSurvey",
                column: "SurveyScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSurvey_SurveyScores_SurveyScoreId",
                table: "GroupSurvey",
                column: "SurveyScoreId",
                principalTable: "SurveyScores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSurvey_SurveyScores_SurveyScoreId",
                table: "GroupSurvey");

            migrationBuilder.DropIndex(
                name: "IX_GroupSurvey_SurveyScoreId",
                table: "GroupSurvey");

            migrationBuilder.DropColumn(
                name: "SurveyScoreId",
                table: "GroupSurvey");
        }
    }
}
