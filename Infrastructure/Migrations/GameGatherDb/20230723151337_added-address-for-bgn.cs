using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedaddressforbgn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "BoardGameNights");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "BoardGameNights",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "BoardGameNights",
                newName: "Street");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "BoardGameNights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
