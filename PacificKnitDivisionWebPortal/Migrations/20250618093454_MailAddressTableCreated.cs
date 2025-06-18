using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PacificKnitDivisionWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class MailAddressTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mailAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SLNo = table.Column<int>(type: "int", nullable: false),
                    DesigId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mailAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mailAddress_department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_mailAddress_designation_DesigId",
                        column: x => x.DesigId,
                        principalTable: "designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mailAddress_DeptId",
                table: "mailAddress",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_mailAddress_DesigId",
                table: "mailAddress",
                column: "DesigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mailAddress");
        }
    }
}
