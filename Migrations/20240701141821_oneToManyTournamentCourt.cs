using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TenisoveTurnaje.Migrations
{
    /// <inheritdoc />
    public partial class oneToManyTournamentCourt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                table: "Tournaments",
                type: "int",
                nullable: false
                );

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CourtId",
                table: "Tournaments",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Courts_CourtId",
                table: "Tournaments",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Courts_CourtId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_CourtId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "Tournaments");
        }
    }
}
