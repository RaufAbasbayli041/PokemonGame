using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class sdserqw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonCategories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonCategories", x => new { x.CategoriesId, x.PokemonsId });
                    table.ForeignKey(
                        name: "FK_PokemonCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonCategories_Pokemons_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    AppearedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WildPokemons_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonSkills",
                columns: table => new
                {
                    PokemonsId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSkills", x => new { x.PokemonsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_PokemonSkills_Pokemons_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonSkills_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    CaughtAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerPokemons_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrainerPokemonId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gyms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gyms_TrainerPokemons_TrainerPokemonId",
                        column: x => x.TrainerPokemonId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WildBattle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WildPokemonId = table.Column<int>(type: "int", nullable: false),
                    TrainerPokemonId = table.Column<int>(type: "int", nullable: false),
                    BattleDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TrainerWin = table.Column<bool>(type: "bit", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WildPokemonId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WildBattle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WildBattle_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WildBattle_TrainerPokemons_TrainerPokemonId",
                        column: x => x.TrainerPokemonId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WildBattle_WildPokemons_WildPokemonId",
                        column: x => x.WildPokemonId,
                        principalTable: "WildPokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WildBattle_WildPokemons_WildPokemonId1",
                        column: x => x.WildPokemonId1,
                        principalTable: "WildPokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerPokemon1Id = table.Column<int>(type: "int", nullable: false),
                    TrainerPokemon2Id = table.Column<int>(type: "int", nullable: false),
                    BattleDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TrainerPokemonWinnerId = table.Column<int>(type: "int", nullable: true),
                    TrainerPokemonLoserId = table.Column<int>(type: "int", nullable: true),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_TrainerPokemons_TrainerPokemon1Id",
                        column: x => x.TrainerPokemon1Id,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_TrainerPokemons_TrainerPokemon2Id",
                        column: x => x.TrainerPokemon2Id,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_TrainerPokemons_TrainerPokemonLoserId",
                        column: x => x.TrainerPokemonLoserId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_TrainerPokemons_TrainerPokemonWinnerId",
                        column: x => x.TrainerPokemonWinnerId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_GymId",
                table: "Battles",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_TrainerPokemon1Id",
                table: "Battles",
                column: "TrainerPokemon1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_TrainerPokemon2Id",
                table: "Battles",
                column: "TrainerPokemon2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_TrainerPokemonLoserId",
                table: "Battles",
                column: "TrainerPokemonLoserId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_TrainerPokemonWinnerId",
                table: "Battles",
                column: "TrainerPokemonWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_LocationId",
                table: "Gyms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_TrainerPokemonId",
                table: "Gyms",
                column: "TrainerPokemonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCategories_PokemonsId",
                table: "PokemonCategories",
                column: "PokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSkills_SkillsId",
                table: "PokemonSkills",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPokemons_PokemonId",
                table: "TrainerPokemons",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPokemons_TrainerId",
                table: "TrainerPokemons",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_WildBattle_LocationId",
                table: "WildBattle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WildBattle_TrainerPokemonId",
                table: "WildBattle",
                column: "TrainerPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_WildBattle_WildPokemonId",
                table: "WildBattle",
                column: "WildPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_WildBattle_WildPokemonId1",
                table: "WildBattle",
                column: "WildPokemonId1");

            migrationBuilder.CreateIndex(
                name: "IX_WildPokemons_LocationId",
                table: "WildPokemons",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WildPokemons_PokemonId",
                table: "WildPokemons",
                column: "PokemonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "PokemonCategories");

            migrationBuilder.DropTable(
                name: "PokemonSkills");

            migrationBuilder.DropTable(
                name: "WildBattle");

            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "WildPokemons");

            migrationBuilder.DropTable(
                name: "TrainerPokemons");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
