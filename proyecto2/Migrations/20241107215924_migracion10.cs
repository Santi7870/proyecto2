using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyecto2.Migrations
{
    /// <inheritdoc />
    public partial class migracion10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "CarritoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoItems_CompraId",
                table: "CarritoItems",
                column: "CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoItems_Compras_CompraId",
                table: "CarritoItems",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoItems_Compras_CompraId",
                table: "CarritoItems");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_CarritoItems_CompraId",
                table: "CarritoItems");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "CarritoItems");
        }
    }
}
