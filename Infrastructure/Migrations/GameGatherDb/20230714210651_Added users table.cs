using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addeduserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_User_UserId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_User_HostId",
                table: "BoardGameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_User_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_User_FoodAndDrinksPreferences_FoodAndDrinksPreferenceId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_FoodAndDrinksPreferenceId",
                table: "Users",
                newName: "IX_Users_FoodAndDrinksPreferenceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_Users_HostId",
                table: "BoardGameNights",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FoodAndDrinksPreferences_FoodAndDrinksPreferenceId",
                table: "Users",
                column: "FoodAndDrinksPreferenceId",
                principalTable: "FoodAndDrinksPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_Users_HostId",
                table: "BoardGameNights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FoodAndDrinksPreferences_FoodAndDrinksPreferenceId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FoodAndDrinksPreferenceId",
                table: "User",
                newName: "IX_User_FoodAndDrinksPreferenceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_User_UserId",
                table: "Attendance",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_User_HostId",
                table: "BoardGameNights",
                column: "HostId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_User_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_FoodAndDrinksPreferences_FoodAndDrinksPreferenceId",
                table: "User",
                column: "FoodAndDrinksPreferenceId",
                principalTable: "FoodAndDrinksPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
