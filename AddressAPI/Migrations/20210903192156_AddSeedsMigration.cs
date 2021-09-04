using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressAPI.Migrations
{
    public partial class AddSeedsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumbering", "PostalCode", "Street" },
                values: new object[] { 1, "Lelystad", "Netherlands", "15 - 21", "8231 KJ", "De Schans" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
