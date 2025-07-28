using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class sdfsdferdswdkjljkl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WildBattle_Locations_LocationId",
                table: "WildBattle");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattle_TrainerPokemons_TrainerPokemonId",
                table: "WildBattle");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattle_WildPokemons_WildPokemonId",
                table: "WildBattle");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattle_WildPokemons_WildPokemonId1",
                table: "WildBattle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WildBattle",
                table: "WildBattle");

            migrationBuilder.RenameTable(
                name: "WildBattle",
                newName: "WildBattles");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattle_WildPokemonId1",
                table: "WildBattles",
                newName: "IX_WildBattles_WildPokemonId1");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattle_WildPokemonId",
                table: "WildBattles",
                newName: "IX_WildBattles_WildPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattle_TrainerPokemonId",
                table: "WildBattles",
                newName: "IX_WildBattles_TrainerPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattle_LocationId",
                table: "WildBattles",
                newName: "IX_WildBattles_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WildBattles",
                table: "WildBattles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BattleTurns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BattleId = table.Column<int>(type: "int", nullable: false),
                    AttackerId = table.Column<int>(type: "int", nullable: false),
                    DefenderId = table.Column<int>(type: "int", nullable: false),
                    BattleAction = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    TurnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleTurns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleTurns_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BattleTurns_TrainerPokemons_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BattleTurns_TrainerPokemons_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleTurns_AttackerId",
                table: "BattleTurns",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleTurns_BattleId",
                table: "BattleTurns",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleTurns_DefenderId",
                table: "BattleTurns",
                column: "DefenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattles_Locations_LocationId",
                table: "WildBattles",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattles_TrainerPokemons_TrainerPokemonId",
                table: "WildBattles",
                column: "TrainerPokemonId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattles_WildPokemons_WildPokemonId",
                table: "WildBattles",
                column: "WildPokemonId",
                principalTable: "WildPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattles_WildPokemons_WildPokemonId1",
                table: "WildBattles",
                column: "WildPokemonId1",
                principalTable: "WildPokemons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WildBattles_Locations_LocationId",
                table: "WildBattles");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattles_TrainerPokemons_TrainerPokemonId",
                table: "WildBattles");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattles_WildPokemons_WildPokemonId",
                table: "WildBattles");

            migrationBuilder.DropForeignKey(
                name: "FK_WildBattles_WildPokemons_WildPokemonId1",
                table: "WildBattles");

            migrationBuilder.DropTable(
                name: "BattleTurns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WildBattles",
                table: "WildBattles");

            migrationBuilder.RenameTable(
                name: "WildBattles",
                newName: "WildBattle");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattles_WildPokemonId1",
                table: "WildBattle",
                newName: "IX_WildBattle_WildPokemonId1");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattles_WildPokemonId",
                table: "WildBattle",
                newName: "IX_WildBattle_WildPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattles_TrainerPokemonId",
                table: "WildBattle",
                newName: "IX_WildBattle_TrainerPokemonId");

            migrationBuilder.RenameIndex(
                name: "IX_WildBattles_LocationId",
                table: "WildBattle",
                newName: "IX_WildBattle_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WildBattle",
                table: "WildBattle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattle_Locations_LocationId",
                table: "WildBattle",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattle_TrainerPokemons_TrainerPokemonId",
                table: "WildBattle",
                column: "TrainerPokemonId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattle_WildPokemons_WildPokemonId",
                table: "WildBattle",
                column: "WildPokemonId",
                principalTable: "WildPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WildBattle_WildPokemons_WildPokemonId1",
                table: "WildBattle",
                column: "WildPokemonId1",
                principalTable: "WildPokemons",
                principalColumn: "Id");
        }
    }
}
