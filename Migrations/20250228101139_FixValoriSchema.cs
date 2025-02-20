using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class FixValoriSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Valori");

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "Valori",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "Valori");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Valori",
                type: "bigint",
                nullable: true);
        }
    }
}
