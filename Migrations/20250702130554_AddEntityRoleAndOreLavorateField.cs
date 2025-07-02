using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRoleAndOreLavorateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ore_lavorate",
                table: "AspNetUsers",
                type: "int",
                maxLength: 1,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_AspNetUsers_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AccountId",
                table: "Roles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AccountId1",
                table: "Roles",
                column: "AccountId1",
                unique: true,
                filter: "[AccountId1] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropColumn(
                name: "Ore_lavorate",
                table: "AspNetUsers");
        }
    }
}
