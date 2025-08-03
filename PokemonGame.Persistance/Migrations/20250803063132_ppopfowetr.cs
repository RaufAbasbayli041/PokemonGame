using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ppopfowetr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_AttackerId",
                table: "BattleTurns");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_DefenderId",
                table: "BattleTurns");

            migrationBuilder.DropColumn(
                name: "TrainerWin",
                table: "WildBattles");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "WildBattles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerPokemonLooserId",
                table: "WildBattles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerPokemonWinnerId",
                table: "WildBattles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Losses",
                table: "TrainerPokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "TrainerPokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_AttackerId",
                table: "BattleTurns",
                column: "AttackerId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_DefenderId",
                table: "BattleTurns",
                column: "DefenderId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_AttackerId",
                table: "BattleTurns");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_DefenderId",
                table: "BattleTurns");

            migrationBuilder.DropColumn(
                name: "TrainerPokemonLooserId",
                table: "WildBattles");

            migrationBuilder.DropColumn(
                name: "TrainerPokemonWinnerId",
                table: "WildBattles");

            migrationBuilder.DropColumn(
                name: "Losses",
                table: "TrainerPokemons");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "TrainerPokemons");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "WildBattles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrainerWin",
                table: "WildBattles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_AttackerId",
                table: "BattleTurns",
                column: "AttackerId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleTurns_TrainerPokemons_DefenderId",
                table: "BattleTurns",
                column: "DefenderId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
