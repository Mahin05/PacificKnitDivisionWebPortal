using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class IPPhoneDisplayOrderTableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SortNo",
                table: "IPPhoneDisplayOrder",
                newName: "DisplayNo");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "IPPhoneDisplayOrder",
                newName: "Unit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "IPPhoneDisplayOrder",
                newName: "DisplayOrder");

            migrationBuilder.RenameColumn(
                name: "DisplayNo",
                table: "IPPhoneDisplayOrder",
                newName: "SortNo");
        }
    }
}
