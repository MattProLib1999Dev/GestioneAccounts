using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class AddTableUnionRoleToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_AspNetUsers_AccountId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "RolesAccounts");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolesAccounts",
                newName: "RolesId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "RolesAccounts",
                newName: "AccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_RoleId",
                table: "RolesAccounts",
                newName: "IX_RolesAccounts_RolesId");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesAccounts",
                table: "RolesAccounts",
                columns: new[] { "AccountsId", "RolesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RolesAccounts_AspNetUsers_AccountsId",
                table: "RolesAccounts",
                column: "AccountsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesAccounts_Roles_RolesId",
                table: "RolesAccounts",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesAccounts_AspNetUsers_AccountsId",
                table: "RolesAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesAccounts_Roles_RolesId",
                table: "RolesAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesAccounts",
                table: "RolesAccounts");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "RolesAccounts",
                newName: "AccountRoles");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "AccountRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "AccountsId",
                table: "AccountRoles",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RolesAccounts_RolesId",
                table: "AccountRoles",
                newName: "IX_AccountRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_AspNetUsers_AccountId",
                table: "AccountRoles",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
