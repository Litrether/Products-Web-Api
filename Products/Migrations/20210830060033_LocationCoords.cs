using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Migrations
{
    public partial class LocationCoords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d150066-f148-4997-9c2b-bb3fe99fc89a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99ffe4af-e809-4c34-85f8-f8b6572901ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dc77bec-8228-430e-bfc2-84594669fd47");

            migrationBuilder.DropColumn(
                name: "LocationURl",
                table: "Providers");

            migrationBuilder.AddColumn<decimal>(
                name: "LocationLat",
                table: "Providers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LocationLong",
                table: "Providers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0101f1d0-16a6-4885-b31f-882a55fabdc4", "3c4f6d71-4291-478f-be8a-cedcd82b0338", "User", "USER" },
                    { "40918622-7cde-4017-a15b-5607c3b9b2c8", "ffcec8ec-ce0a-437d-9645-6b71eefd7b81", "Manager", "MANAGER" },
                    { "8d529479-9a47-4ea8-8639-7447a9f76694", "9d357a01-5c1c-4c92-8b68-52ace90a47ff", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 53.89019010647972m, 27.575736202063215m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 38.54213540495325m, 27.033468297986936m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 40.73105861912476m, 46.27047156919906m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 54.26659741177842m, 30.98771355605172m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 49.764727469041816m, 43.65468679640968m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 47.46106041862809m, -122.26236529486663m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 39.16566430628417m, -0.2430474019997804m });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "LocationLat", "LocationLong" },
                values: new object[] { 10.158678793639453m, -10.753070951045318m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0101f1d0-16a6-4885-b31f-882a55fabdc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40918622-7cde-4017-a15b-5607c3b9b2c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d529479-9a47-4ea8-8639-7447a9f76694");

            migrationBuilder.DropColumn(
                name: "LocationLat",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "LocationLong",
                table: "Providers");

            migrationBuilder.AddColumn<string>(
                name: "LocationURl",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d150066-f148-4997-9c2b-bb3fe99fc89a", "5b0302e4-4561-4aa8-a66a-0a4a8fa28049", "User", "USER" },
                    { "99ffe4af-e809-4c34-85f8-f8b6572901ab", "4361d154-4598-46d8-b191-519591a60cd9", "Manager", "MANAGER" },
                    { "9dc77bec-8228-430e-bfc2-84594669fd47", "d6f6a4a9-da59-437c-9cdd-60aebfe2393a", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2245.0599144050398!2d37.633201316046446!3d55.75746139909995!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46b54bc6463a7e1f%3A0xd68e89e31b1d749d!2sUnderdog!5e0!3m2!1sru!2sby!4v1629892468792!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24955.706297501256!2d27.025704972171763!3d38.56917789967588!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd0550cc74b71%3A0x1187e14414fd380b!2sAtha%20Pack!5e0!3m2!1sru!2sby!4v1629892445596!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3023.4065496478047!2d46.268293615655864!3d40.73107894438454!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x403f5703b931bbad%3A0x6961e67ff6f0fa5b!2zU0jEsFLEsE4gQUdSTw!5e0!3m2!1sru!2sby!4v1629892427348!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2217.634110197547!2d43.877038416060536!3d56.23255746235498!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x414e2b0cf172c9f5%3A0x52c68ec4e6912c0c!2z0JzQvtC70L7Rh9C90LDRjyDRgNC10LrQsA!5e0!3m2!1sru!2sby!4v1629892401840!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d329288.70255430153!2d43.46470878878752!3d49.8505744089535!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4110ae4940ccb62b%3A0xbcc910b5c1d192f1!2z0JDRgNGH0LXQtNCw!5e0!3m2!1sru!2sby!4v1629892371206!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1514194.156913194!2d-8.354596788586328!3d42.16290088735141!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2585ac9f614b59%3A0x5fa1a18a4537ab19!2sLeche%20Pascual!5e0!3m2!1sru!2sby!4v1629892347122!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2940.5620697896807!2d-8.779720084303564!3d42.5221136328442!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2f6b3162dc2bb7%3A0x7393d35e4abdf3fb!2sAlimentos%20Javimar%20S.L.!5e0!3m2!1sru!2sby!4v1629892308672!5m2!1sru!2sby");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocationURl",
                value: "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d11191.06220539455!2d25.314657263806758!3d53.86720882934107!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46de8adedfc9ddfd%3A0x2120bcee12937ed1!2z0JvQuNC00YHQutC40Lkg0LzQvtC70L7Rh9C90L4t0LrQvtC90YHQtdGA0LLQvdGL0Lkg0LrQvtC80LHQuNC90LDRgg!5e0!3m2!1sru!2sby!4v1629891953077!5m2!1sru!2sby");
        }
    }
}
