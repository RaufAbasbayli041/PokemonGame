using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class dfedwf34r : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_TrainerPokemons_TrainerPokemonId",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "TrainerPokemonId",
                table: "Gyms",
                newName: "LeaderTrainerPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_Gyms_TrainerPokemonId",
                table: "Gyms",
                newName: "IX_Gyms_LeaderTrainerPokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_TrainerPokemons_LeaderTrainerPokemonId",
                table: "Gyms",
                column: "LeaderTrainerPokemonId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_TrainerPokemons_LeaderTrainerPokemonId",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "LeaderTrainerPokemonId",
                table: "Gyms",
                newName: "TrainerPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_Gyms_LeaderTrainerPokemonId",
                table: "Gyms",
                newName: "IX_Gyms_TrainerPokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_TrainerPokemons_TrainerPokemonId",
                table: "Gyms",
                column: "TrainerPokemonId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id");
        }
    }
}
