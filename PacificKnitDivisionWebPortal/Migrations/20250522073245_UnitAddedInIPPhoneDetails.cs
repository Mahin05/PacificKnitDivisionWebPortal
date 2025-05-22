using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class UnitAddedInIPPhoneDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ipphoneDetails");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "ipphoneDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ipphoneDetails_UnitId",
                table: "ipphoneDetails",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ipphoneDetails_unit_UnitId",
                table: "ipphoneDetails",
                column: "UnitId",
                principalTable: "unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ipphoneDetails_unit_UnitId",
                table: "ipphoneDetails");

            migrationBuilder.DropIndex(
                name: "IX_ipphoneDetails_UnitId",
                table: "ipphoneDetails");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ipphoneDetails");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ipphoneDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
