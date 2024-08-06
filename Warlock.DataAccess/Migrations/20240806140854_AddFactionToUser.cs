using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warlock.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFactionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FactionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FactionId",
                table: "AspNetUsers",
                column: "FactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Factions_FactionId",
                table: "AspNetUsers",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Factions_FactionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FactionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FactionId",
                table: "AspNetUsers");
        }
    }
}
