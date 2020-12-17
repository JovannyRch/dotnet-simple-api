using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiDePrueba.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Libros_LibroId",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_LibroId",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "LibroId",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Libros",
                newName: "Titulo");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutorId",
                table: "Libros",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autores_AutorId",
                table: "Libros",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autores_AutorId",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_AutorId",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Libros",
                newName: "Nombre");

            migrationBuilder.AddColumn<int>(
                name: "LibroId",
                table: "Libros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_LibroId",
                table: "Libros",
                column: "LibroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Libros_LibroId",
                table: "Libros",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
