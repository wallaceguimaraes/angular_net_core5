using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacao.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    SexoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexos", x => x.SexoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Senha = table.Column<string>(type: "varchar(30)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    SexoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Sexos_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexos",
                        principalColumn: "SexoId");
                });

            migrationBuilder.InsertData(
                table: "Sexos",
                columns: new[] { "SexoId", "Descricao" },
                values: new object[] { 1, "Masculino" });

            migrationBuilder.InsertData(
                table: "Sexos",
                columns: new[] { "SexoId", "Descricao" },
                values: new object[] { 2, "Feminino" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SexoId",
                table: "Usuarios",
                column: "SexoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sexos");
        }
    }
}
