using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseAPI.Repository.Migrations
{
    public partial class FixMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonType_Pokemon_PokemonId",
                table: "PokemonType");

            migrationBuilder.DropIndex(
                name: "IX_PokemonType_PokemonId",
                table: "PokemonType");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "PokemonType");

            migrationBuilder.CreateTable(
                name: "PokemonPokemonType",
                columns: table => new
                {
                    PokemonsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonPokemonType", x => new { x.PokemonsId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_PokemonPokemonType_Pokemon_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonPokemonType_PokemonType_TypesId",
                        column: x => x.TypesId,
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPokemonType_TypesId",
                table: "PokemonPokemonType",
                column: "TypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonPokemonType");

            migrationBuilder.AddColumn<Guid>(
                name: "PokemonId",
                table: "PokemonType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonType_PokemonId",
                table: "PokemonType",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonType_Pokemon_PokemonId",
                table: "PokemonType",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id");
        }
    }
}
