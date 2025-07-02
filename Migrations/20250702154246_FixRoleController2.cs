using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleController2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_AspNetUsers_AccountId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_AspNetUsers_AccountId1",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AccountId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AccountId1",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Roles_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Roles_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Roles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_AspNetUsers_AccountId",
                table: "Roles",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_AspNetUsers_AccountId1",
                table: "Roles",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
