using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class deptTableissue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "testColumn",
                table: "department",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "testColumn",
                table: "department");
        }
    }
}
