using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integrativa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "processos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Assunto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "andamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcessoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_andamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_andamentos_processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "processos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcessoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TipoParte = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partes_processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "processos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_andamentos_ProcessoId_DataCriacao",
                table: "andamentos",
                columns: new[] { "ProcessoId", "DataCriacao" });

            migrationBuilder.CreateIndex(
                name: "IX_partes_ProcessoId",
                table: "partes",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_processos_Numero",
                table: "processos",
                column: "Numero",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "andamentos");

            migrationBuilder.DropTable(
                name: "partes");

            migrationBuilder.DropTable(
                name: "processos");
        }
    }
}
