using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPoke.API.Migrations
{
    public partial class AddGrowthRateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrowthRateLevelExperience_GrowthRate_ParentGrowthRateId",
                table: "GrowthRateLevelExperience");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSpecies_GrowthRate_GrowthRateId",
                table: "PokemonSpecies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrowthRateLevelExperience",
                table: "GrowthRateLevelExperience");

            migrationBuilder.DropIndex(
                name: "IX_GrowthRateLevelExperience_ParentGrowthRateId",
                table: "GrowthRateLevelExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrowthRate",
                table: "GrowthRate");

            migrationBuilder.DropColumn(
                name: "ParentGrowthRateId",
                table: "GrowthRateLevelExperience");

            migrationBuilder.RenameTable(
                name: "GrowthRateLevelExperience",
                newName: "GrowthRateLevelExperiences");

            migrationBuilder.RenameTable(
                name: "GrowthRate",
                newName: "GrowthRates");

            migrationBuilder.AddColumn<int>(
                name: "GrowthRateId",
                table: "GrowthRateLevelExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrowthRateLevelExperiences",
                table: "GrowthRateLevelExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrowthRates",
                table: "GrowthRates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthRateLevelExperiences_GrowthRateId",
                table: "GrowthRateLevelExperiences",
                column: "GrowthRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrowthRateLevelExperiences_GrowthRates_GrowthRateId",
                table: "GrowthRateLevelExperiences",
                column: "GrowthRateId",
                principalTable: "GrowthRates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSpecies_GrowthRates_GrowthRateId",
                table: "PokemonSpecies",
                column: "GrowthRateId",
                principalTable: "GrowthRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrowthRateLevelExperiences_GrowthRates_GrowthRateId",
                table: "GrowthRateLevelExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSpecies_GrowthRates_GrowthRateId",
                table: "PokemonSpecies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrowthRates",
                table: "GrowthRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrowthRateLevelExperiences",
                table: "GrowthRateLevelExperiences");

            migrationBuilder.DropIndex(
                name: "IX_GrowthRateLevelExperiences_GrowthRateId",
                table: "GrowthRateLevelExperiences");

            migrationBuilder.DropColumn(
                name: "GrowthRateId",
                table: "GrowthRateLevelExperiences");

            migrationBuilder.RenameTable(
                name: "GrowthRates",
                newName: "GrowthRate");

            migrationBuilder.RenameTable(
                name: "GrowthRateLevelExperiences",
                newName: "GrowthRateLevelExperience");

            migrationBuilder.AddColumn<int>(
                name: "ParentGrowthRateId",
                table: "GrowthRateLevelExperience",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrowthRate",
                table: "GrowthRate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrowthRateLevelExperience",
                table: "GrowthRateLevelExperience",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthRateLevelExperience_ParentGrowthRateId",
                table: "GrowthRateLevelExperience",
                column: "ParentGrowthRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrowthRateLevelExperience_GrowthRate_ParentGrowthRateId",
                table: "GrowthRateLevelExperience",
                column: "ParentGrowthRateId",
                principalTable: "GrowthRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSpecies_GrowthRate_GrowthRateId",
                table: "PokemonSpecies",
                column: "GrowthRateId",
                principalTable: "GrowthRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
