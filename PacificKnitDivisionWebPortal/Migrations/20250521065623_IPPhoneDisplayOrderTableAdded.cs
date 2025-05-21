using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class IPPhoneDisplayOrderTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPPhoneDisplayOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortNo = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPPhoneID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPPhoneDisplayOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPPhoneDisplayOrder_ipphoneDetails_IPPhoneID",
                        column: x => x.IPPhoneID,
                        principalTable: "ipphoneDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPPhoneDisplayOrder_IPPhoneID",
                table: "IPPhoneDisplayOrder",
                column: "IPPhoneID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPPhoneDisplayOrder");
        }
    }
}
