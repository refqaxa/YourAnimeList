using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourAnimeList.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Animes");
        }
    }
}
