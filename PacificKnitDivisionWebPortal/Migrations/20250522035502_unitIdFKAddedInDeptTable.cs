using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class unitIdFKAddedInDeptTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "department",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_department_UnitId",
                table: "department",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_department_unit_UnitId",
                table: "department",
                column: "UnitId",
                principalTable: "unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_unit_UnitId",
                table: "department");

            migrationBuilder.DropIndex(
                name: "IX_department_UnitId",
                table: "department");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "department");
        }
    }
}
