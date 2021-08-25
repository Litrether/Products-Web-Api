using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Migrations
{
    public partial class addLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e54bd3d-4581-4ed3-b416-034a55ff0f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad96eab0-89e6-4674-abdf-b7749916f434");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7e61b4c-e7e1-484c-8e73-175c74cfd2bf");

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
                    { "d3608d6d-c14d-41b7-ac16-c4a0c45450d9", "1e96aabe-a603-4517-b065-7fb638eddf43", "User", "USER" },
                    { "1e9c7902-a4b2-4de8-b6c6-685d8ceb25d0", "f3a47874-a9ed-49e5-873f-6c4c03cff1e9", "Manager", "MANAGER" },
                    { "6dc7b140-2706-4369-83b7-d80e8d1eda99", "9bd3db65-693f-46b2-badb-aa6739951657", "Administrator", "ADMINISTRATOR" }
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e9c7902-a4b2-4de8-b6c6-685d8ceb25d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dc7b140-2706-4369-83b7-d80e8d1eda99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3608d6d-c14d-41b7-ac16-c4a0c45450d9");

            migrationBuilder.DropColumn(
                name: "LocationURl",
                table: "Providers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad96eab0-89e6-4674-abdf-b7749916f434", "f789d806-74fe-404c-b43f-bca3f1e8edca", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7e61b4c-e7e1-484c-8e73-175c74cfd2bf", "095a012e-ac48-4c7f-9859-56d4ddc477e6", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e54bd3d-4581-4ed3-b416-034a55ff0f3f", "8a8e63bf-25ac-43b8-b8b0-d62abd5e7b7d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
