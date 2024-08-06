using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Warlock.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedFactionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Factions",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Faction City", "Traders", "0123456789", "1234", "Faction State", "123 Faction St" },
                    { 2, "Faction City", "Explorers", "0123456789", "1234", "Faction State", "123 Faction St" },
                    { 3, "Faction City", "Mercenaries", "0123456789", "1234", "Faction State", "123 Faction St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Factions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
