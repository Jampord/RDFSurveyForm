using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class GroupSurveyScoreGenerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyGenerator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyGenerator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupsId = table.Column<int>(type: "int", nullable: true),
                    SurveyGeneratorId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTransacted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSurvey_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupSurvey_SurveyGenerator_SurveyGeneratorId",
                        column: x => x.SurveyGeneratorId,
                        principalTable: "SurveyGenerator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    SurveyGeneratorId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),

                  
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyScores", x => x.Id);
                 
                
                    table.ForeignKey(
                        name: "FK_SurveyScores_SurveyGenerator_SurveyGeneratorId",
                        column: x => x.SurveyGeneratorId,
                        principalTable: "SurveyGenerator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSurvey_GroupsId",
                table: "GroupSurvey",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSurvey_SurveyGeneratorId",
                table: "GroupSurvey",
                column: "SurveyGeneratorId");




            migrationBuilder.CreateIndex(
                name: "IX_SurveyScores_SurveyGeneratorId",
                table: "SurveyScores",
                column: "SurveyGeneratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupSurvey");

            migrationBuilder.DropTable(
                name: "SurveyScores");

            migrationBuilder.DropTable(
                name: "SurveyGenerator");
        }
    }
}
