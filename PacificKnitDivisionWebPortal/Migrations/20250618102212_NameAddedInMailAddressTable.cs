using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class NameAddedInMailAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLNo",
                table: "mailAddress");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "mailAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "mailAddress");

            migrationBuilder.AddColumn<int>(
                name: "SLNo",
                table: "mailAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
