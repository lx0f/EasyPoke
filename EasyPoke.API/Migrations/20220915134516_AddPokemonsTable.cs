using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPoke.API.Migrations
{
    public partial class AddPokemonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_PokemonSpecies_PokemonSpeciesId",
                table: "Pokemon");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_Users_UserId",
                table: "Pokemon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pokemon",
                table: "Pokemon");

            migrationBuilder.RenameTable(
                name: "Pokemon",
                newName: "Pokemons");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_UserId",
                table: "Pokemons",
                newName: "IX_Pokemons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemon_PokemonSpeciesId",
                table: "Pokemons",
                newName: "IX_Pokemons_PokemonSpeciesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pokemons",
                table: "Pokemons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_PokemonSpecies_PokemonSpeciesId",
                table: "Pokemons",
                column: "PokemonSpeciesId",
                principalTable: "PokemonSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Users_UserId",
                table: "Pokemons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_PokemonSpecies_PokemonSpeciesId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Users_UserId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pokemons",
                table: "Pokemons");

            migrationBuilder.RenameTable(
                name: "Pokemons",
                newName: "Pokemon");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemons_UserId",
                table: "Pokemon",
                newName: "IX_Pokemon_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemons_PokemonSpeciesId",
                table: "Pokemon",
                newName: "IX_Pokemon_PokemonSpeciesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pokemon",
                table: "Pokemon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_PokemonSpecies_PokemonSpeciesId",
                table: "Pokemon",
                column: "PokemonSpeciesId",
                principalTable: "PokemonSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_Users_UserId",
                table: "Pokemon",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
