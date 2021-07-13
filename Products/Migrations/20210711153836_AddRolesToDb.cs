using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Migrations
{
    public partial class AddRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "188fa276-9d93-4e6c-b9c3-1b1c786de049", "4f6ba7d5-6882-4a15-911c-e706d0aeffe3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "447a14f9-12a0-44f8-877e-0f983efa1f60", "f38b7317-b367-4c9e-8a5e-3a79f4589ae3", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8facbe4-3201-4576-b18c-308f1b498984", "b061c0bb-3620-478d-aa50-9769169d14c1", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "188fa276-9d93-4e6c-b9c3-1b1c786de049");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "447a14f9-12a0-44f8-877e-0f983efa1f60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8facbe4-3201-4576-b18c-308f1b498984");
        }
    }
}
