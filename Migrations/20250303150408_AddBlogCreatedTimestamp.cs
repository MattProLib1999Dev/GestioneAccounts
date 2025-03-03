using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneAccounts.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogCreatedTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valoreString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    voce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Valore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValoreStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValoreString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valore_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Valore_AccountId",
                table: "Valore",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Valore");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
