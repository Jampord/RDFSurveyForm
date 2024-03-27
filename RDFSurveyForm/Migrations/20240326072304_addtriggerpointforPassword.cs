using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class addtriggerpointforPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UpdatePass",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatePass",
                table: "Customer");
        }
    }
}
