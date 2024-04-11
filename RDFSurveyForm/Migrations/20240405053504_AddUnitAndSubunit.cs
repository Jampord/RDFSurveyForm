using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDFSurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitAndSubunit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Department",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubunitId",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SubunitId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CRole",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "CRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Subunits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubunitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subunits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_SubunitId",
                table: "Department",
                column: "SubunitId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_UnitId",
                table: "Department",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SubunitId",
                table: "Customer",
                column: "SubunitId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UnitId",
                table: "Customer",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Subunits_SubunitId",
                table: "Customer",
                column: "SubunitId",
                principalTable: "Subunits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Units_UnitId",
                table: "Customer",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Subunits_SubunitId",
                table: "Department",
                column: "SubunitId",
                principalTable: "Subunits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Units_UnitId",
                table: "Department",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Subunits_SubunitId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Units_UnitId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Subunits_SubunitId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Units_UnitId",
                table: "Department");

            migrationBuilder.DropTable(
                name: "Subunits");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Department_SubunitId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_UnitId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Customer_SubunitId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UnitId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "SubunitId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SubunitId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CRole");

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "CRole");
        }
    }
}
