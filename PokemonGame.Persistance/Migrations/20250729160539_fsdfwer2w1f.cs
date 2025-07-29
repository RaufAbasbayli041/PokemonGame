using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fsdfwer2w1f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialAttack",
                table: "PokemonBaseStats");

            migrationBuilder.DropColumn(
                name: "SpecialDefense",
                table: "PokemonBaseStats");

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "SpecialAttack",
                table: "PokemonBaseStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialDefense",
                table: "PokemonBaseStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
