 using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitizenTech.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NET_PONTO_ONIBUS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_PONTO_ONIBUS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NET_CHEGADA_ONIBUS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Linha = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Destino = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PrevisaoChegada = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PontoOnibusId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_CHEGADA_ONIBUS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NET_CHEGADA_ONIBUS_NET_PONTO_ONIBUS_PontoOnibusId",
                        column: x => x.PontoOnibusId,
                        principalTable: "NET_PONTO_ONIBUS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NET_CHEGADA_ONIBUS_PontoOnibusId",
                table: "NET_CHEGADA_ONIBUS",
                column: "PontoOnibusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_CHEGADA_ONIBUS");

            migrationBuilder.DropTable(
                name: "NET_PONTO_ONIBUS");
        }
    }
}
