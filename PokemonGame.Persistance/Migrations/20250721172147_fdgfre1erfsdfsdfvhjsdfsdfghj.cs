using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fdgfre1erfsdfsdfvhjsdfsdfghj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battle_Gyms_GymId",
                table: "Battle");

            migrationBuilder.DropForeignKey(
                name: "FK_Battle_Trainers_Trainer1Id",
                table: "Battle");

            migrationBuilder.DropForeignKey(
                name: "FK_Battle_Trainers_Trainer2Id",
                table: "Battle");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Location_LocationId",
                table: "Gyms");

            migrationBuilder.DropTable(
                name: "PokemonTrainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Battle",
                table: "Battle");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Battle",
                newName: "Battles");

            migrationBuilder.RenameIndex(
                name: "IX_Battle_Trainer2Id",
                table: "Battles",
                newName: "IX_Battles_Trainer2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Battle_Trainer1Id",
                table: "Battles",
                newName: "IX_Battles_Trainer1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Battle_GymId",
                table: "Battles",
                newName: "IX_Battles_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battles",
                table: "Battles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TrainerPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    CaughtAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CurrentHP = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPokemons_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TrainerPokemons_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WildPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    AppearedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WildPokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WildPokemons_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WildPokemons_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPokemons_PokemonId",
                table: "TrainerPokemons",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPokemons_TrainerId",
                table: "TrainerPokemons",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_WildPokemons_LocationId",
                table: "WildPokemons",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WildPokemons_PokemonId",
                table: "WildPokemons",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_Trainer1Id",
                table: "Battles",
                column: "Trainer1Id",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_Trainer2Id",
                table: "Battles",
                column: "Trainer2Id",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Locations_LocationId",
                table: "Gyms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_Trainer1Id",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_Trainer2Id",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Locations_LocationId",
                table: "Gyms");

            migrationBuilder.DropTable(
                name: "TrainerPokemons");

            migrationBuilder.DropTable(
                name: "WildPokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Battles",
                table: "Battles");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Battles",
                newName: "Battle");

            migrationBuilder.RenameIndex(
                name: "IX_Battles_Trainer2Id",
                table: "Battle",
                newName: "IX_Battle_Trainer2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Battles_Trainer1Id",
                table: "Battle",
                newName: "IX_Battle_Trainer1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Battles_GymId",
                table: "Battle",
                newName: "IX_Battle_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battle",
                table: "Battle",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PokemonTrainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CaughtAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentHP = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTrainer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTrainer_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PokemonTrainer_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTrainer_PokemonId",
                table: "PokemonTrainer",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTrainer_TrainerId",
                table: "PokemonTrainer",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battle_Gyms_GymId",
                table: "Battle",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Battle_Trainers_Trainer1Id",
                table: "Battle",
                column: "Trainer1Id",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Battle_Trainers_Trainer2Id",
                table: "Battle",
                column: "Trainer2Id",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Location_LocationId",
                table: "Gyms",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
