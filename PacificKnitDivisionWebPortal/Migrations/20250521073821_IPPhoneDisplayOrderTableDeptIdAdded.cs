using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class IPPhoneDisplayOrderTableDeptIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPPhoneDisplayOrder_ipphoneDetails_IPPhoneID",
                table: "IPPhoneDisplayOrder");

            migrationBuilder.RenameColumn(
                name: "IPPhoneID",
                table: "IPPhoneDisplayOrder",
                newName: "DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_IPPhoneDisplayOrder_IPPhoneID",
                table: "IPPhoneDisplayOrder",
                newName: "IX_IPPhoneDisplayOrder_DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_IPPhoneDisplayOrder_department_DeptId",
                table: "IPPhoneDisplayOrder",
                column: "DeptId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPPhoneDisplayOrder_department_DeptId",
                table: "IPPhoneDisplayOrder");

            migrationBuilder.RenameColumn(
                name: "DeptId",
                table: "IPPhoneDisplayOrder",
                newName: "IPPhoneID");

            migrationBuilder.RenameIndex(
                name: "IX_IPPhoneDisplayOrder_DeptId",
                table: "IPPhoneDisplayOrder",
                newName: "IX_IPPhoneDisplayOrder_IPPhoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_IPPhoneDisplayOrder_ipphoneDetails_IPPhoneID",
                table: "IPPhoneDisplayOrder",
                column: "IPPhoneID",
                principalTable: "ipphoneDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
