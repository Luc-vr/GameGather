using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Users",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "Street");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
