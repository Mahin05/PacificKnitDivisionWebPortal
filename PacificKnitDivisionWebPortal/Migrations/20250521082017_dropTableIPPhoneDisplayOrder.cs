using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class dropTableIPPhoneDisplayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPPhoneDisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPPhoneDisplayOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    DisplayNo = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPPhoneDisplayOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPPhoneDisplayOrder_department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPPhoneDisplayOrder_DeptId",
                table: "IPPhoneDisplayOrder",
                column: "DeptId");
        }
    }
}
