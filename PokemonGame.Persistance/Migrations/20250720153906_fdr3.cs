using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fdr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTrainer_Pokemons_PokemonsId",
                table: "PokemonTrainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonTrainer",
                table: "PokemonTrainer");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_CategoryId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "BadgeCollection",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pokemons");

            migrationBuilder.RenameColumn(
                name: "PokemonsId",
                table: "PokemonTrainer",
                newName: "PokemonId");

            migrationBuilder.AddColumn<int>(
                name: "TrainerLevel",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PokemonTrainer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CaughtAt",
                table: "PokemonTrainer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PokemonTrainer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrentHP",
                table: "PokemonTrainer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PokemonTrainer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "PokemonTrainer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PokemonTrainer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonTrainer",
                table: "PokemonTrainer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trainer1Id = table.Column<int>(type: "int", nullable: false),
                    Trainer2Id = table.Column<int>(type: "int", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battle_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battle_Trainers_Trainer1Id",
                        column: x => x.Trainer1Id,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battle_Trainers_Trainer2Id",
                        column: x => x.Trainer2Id,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPokemon",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPokemon", x => new { x.CategoriesId, x.PokemonsId });
                    table.ForeignKey(
                        name: "FK_CategoryPokemon_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPokemon_Pokemons_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTrainer_PokemonId",
                table: "PokemonTrainer",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_LeaderId",
                table: "Gyms",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_LocationId",
                table: "Gyms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_GymId",
                table: "Battle",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Trainer1Id",
                table: "Battle",
                column: "Trainer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Trainer2Id",
                table: "Battle",
                column: "Trainer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPokemon_PokemonsId",
                table: "CategoryPokemon",
                column: "PokemonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Location_LocationId",
                table: "Gyms",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Trainers_LeaderId",
                table: "Gyms",
                column: "LeaderId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTrainer_Pokemons_PokemonId",
                table: "PokemonTrainer",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Location_LocationId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Trainers_LeaderId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTrainer_Pokemons_PokemonId",
                table: "PokemonTrainer");

            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "CategoryPokemon");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonTrainer",
                table: "PokemonTrainer");

            migrationBuilder.DropIndex(
                name: "IX_PokemonTrainer_PokemonId",
                table: "PokemonTrainer");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_LeaderId",
                table: "Gyms");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_LocationId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "TrainerLevel",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "CaughtAt",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "CurrentHP",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PokemonTrainer");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "PokemonTrainer",
                newName: "PokemonsId");

            migrationBuilder.AddColumn<string>(
                name: "BadgeCollection",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonTrainer",
                table: "PokemonTrainer",
                columns: new[] { "PokemonsId", "TrainerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_CategoryId",
                table: "Pokemons",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTrainer_Pokemons_PokemonsId",
                table: "PokemonTrainer",
                column: "PokemonsId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
