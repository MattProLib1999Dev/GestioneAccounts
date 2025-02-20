using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIdToValori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "Valori");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Valori",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCreazione",
                table: "Valori",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Valori");

            migrationBuilder.DropColumn(
                name: "DataCreazione",
                table: "Valori");

            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "Valori",
                type: "int",
                nullable: true);
        }
    }
}
