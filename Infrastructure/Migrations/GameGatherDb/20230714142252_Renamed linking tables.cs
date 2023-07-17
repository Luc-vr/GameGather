using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Renamedlinkingtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameBoardGameNight");

            migrationBuilder.DropTable(
                name: "BoardGameNightUser");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.UserId, x.BoardGameNightId });
                    table.ForeignKey(
                        name: "FK_Attendance_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayedBoardGames",
                columns: table => new
                {
                    BoardGameId = table.Column<int>(type: "int", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedBoardGames", x => new { x.BoardGameId, x.BoardGameNightId });
                    table.ForeignKey(
                        name: "FK_PlayedBoardGames_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayedBoardGames_BoardGames_BoardGameId",
                        column: x => x.BoardGameId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_BoardGameNightId",
                table: "Attendance",
                column: "BoardGameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayedBoardGames_BoardGameNightId",
                table: "PlayedBoardGames",
                column: "BoardGameNightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "PlayedBoardGames");

            migrationBuilder.CreateTable(
                name: "BoardGameBoardGameNight",
                columns: table => new
                {
                    BoardGameNightsId = table.Column<int>(type: "int", nullable: false),
                    BoardGamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameBoardGameNight", x => new { x.BoardGameNightsId, x.BoardGamesId });
                    table.ForeignKey(
                        name: "FK_BoardGameBoardGameNight_BoardGameNights_BoardGameNightsId",
                        column: x => x.BoardGameNightsId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameBoardGameNight_BoardGames_BoardGamesId",
                        column: x => x.BoardGamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameNightUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameNightUser", x => new { x.UserId, x.BoardGameNightId });
                    table.ForeignKey(
                        name: "FK_BoardGameNightUser_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameNightUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameBoardGameNight_BoardGamesId",
                table: "BoardGameBoardGameNight",
                column: "BoardGamesId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameNightUser_BoardGameNightId",
                table: "BoardGameNightUser",
                column: "BoardGameNightId");
        }
    }
}
