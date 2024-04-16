using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class userAndGroupRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GroupsId",
                table: "Customer",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Groups_GroupsId",
                table: "Customer",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Groups_GroupsId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GroupsId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                table: "Customer");
        }
    }
}
