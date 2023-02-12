using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseAPI.Repository.Migrations
{
    public partial class FixMappingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonTypeMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PokemonTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypeMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTypeMapping_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTypeMapping_PokemonType_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeMapping_PokemonId",
                table: "PokemonTypeMapping",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeMapping_PokemonTypeId",
                table: "PokemonTypeMapping",
                column: "PokemonTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonTypeMapping");
        }
    }
}
