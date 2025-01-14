using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.AccountDb
{
    /// <inheritdoc />
    public partial class renameuserseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb1", 0, "e3fe82d4-aee3-4aca-a378-0a270535db29", "DJK@beats.nl", false, false, null, "DJK@BEATS.NL", "DJK@BEATS.NL", "AQAAAAEAACcQAAAAEGCj7V52STAgZXbiQ5WfoUnWGCjgsjxZN+Zv+bAnlYDhQgOKMsRizrx8QdPX/GQyAg==", null, false, "3ad77dd9-5578-4ae1-8c62-bfc70949cbb7", false, "DJK@beats.nl" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb2", 0, "eaa0d4cc-7440-4977-8e35-76275ff3037f", "runescape@chris.nl", false, false, null, "RUNESCAPE@CHRIS.NL", "RUNESCAPE@CHRIS.NL", "AQAAAAEAACcQAAAAEBYSkSR1OhyZC68B5y7xnI1HvWwQrbcgkRcj3RR5FhXALh4OS8c/ZdziIB2DGGuGBg==", null, false, "f4ad1c5b-9052-437a-aba7-c88be376ebd9", false, "runescape@chris.nl" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb3", 0, "fae97885-639f-4048-bd79-8414ed7d2148", "breda@janou.nl", false, false, null, "BREDA@JANOU.NL", "BREDA@JANOU.NL", "AQAAAAEAACcQAAAAEI38+Nfwm5o8AqNTlsDQ+zwsbinbLIbOJK0xeSh3KIM5gv5th8/RqShVolwDg9Nb/w==", null, false, "837668a0-fe4c-4aaf-b300-6d13a8c3f247", false, "breda@janou.nl" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb4", 0, "f63bb8ad-c925-4df9-acc1-43d4aec8989c", "jelmar@geld.nl", false, false, null, "JELMAR@GELD.NL", "JELMAR@GELD.NL", "AQAAAAEAACcQAAAAEOLUCHNxqDbp/FIJ61tz2eZToKeSXGnN/uah//d2Eqe6xb6fwnYBmOf692nzNd87tg==", null, false, "85c0f20e-3d2d-40fb-b46e-310bcb8dabe2", false, "jelmar@geld.nl" }
                });
        }
    }
}
