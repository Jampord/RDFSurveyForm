using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class addrealtiontoUNitAndSUbunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Subunits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subunits_UnitId",
                table: "Subunits",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subunits_Units_UnitId",
                table: "Subunits",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subunits_Units_UnitId",
                table: "Subunits");

            migrationBuilder.DropIndex(
                name: "IX_Subunits_UnitId",
                table: "Subunits");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Subunits");
        }
    }
}
