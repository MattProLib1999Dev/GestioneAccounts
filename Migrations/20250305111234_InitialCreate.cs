using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValoreString",
                table: "Valore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValoreString",
                table: "Valore",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
