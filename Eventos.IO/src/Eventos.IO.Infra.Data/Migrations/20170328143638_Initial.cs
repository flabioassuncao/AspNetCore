using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventos.IO.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CPF = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: true),
                    DataFim = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    varcharmax = table.Column<string>(name: "varchar(max)", nullable: true),
                    DescricaoLonga = table.Column<string>(nullable: true),
                    EnderecoId = table.Column<Guid>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Gratuito = table.Column<bool>(nullable: false),
                    varchar150 = table.Column<string>(name: "varchar(150)", nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    OrganizadorId = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Bairro = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    EventoId = table.Column<Guid>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_EventoId",
                table: "Enderecos",
                column: "EventoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CategoriaId",
                table: "Eventos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_OrganizadorId",
                table: "Eventos",
                column: "OrganizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "Organizadores");
        }
    }
}
