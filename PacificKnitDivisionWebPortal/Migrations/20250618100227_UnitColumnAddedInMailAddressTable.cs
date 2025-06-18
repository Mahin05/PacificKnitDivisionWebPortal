using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class UnitColumnAddedInMailAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "mailAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_mailAddress_UnitId",
                table: "mailAddress",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_mailAddress_unit_UnitId",
                table: "mailAddress",
                column: "UnitId",
                principalTable: "unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mailAddress_unit_UnitId",
                table: "mailAddress");

            migrationBuilder.DropIndex(
                name: "IX_mailAddress_UnitId",
                table: "mailAddress");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "mailAddress");
        }
    }
}
