using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class UnitDisplayNoAddedInDepartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayNo",
                table: "department",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "department",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayNo",
                table: "department");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "department");
        }
    }
}
