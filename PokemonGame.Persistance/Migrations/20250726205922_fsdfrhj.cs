using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGame.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fsdfrhj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWild",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWild",
                table: "Pokemons");
        }
    }
}
