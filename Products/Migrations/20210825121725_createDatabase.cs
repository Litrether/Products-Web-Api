using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Products.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationURl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9dc77bec-8228-430e-bfc2-84594669fd47", "d6f6a4a9-da59-437c-9cdd-60aebfe2393a", "Administrator", "ADMINISTRATOR" },
                    { "4d150066-f148-4997-9c2b-bb3fe99fc89a", "5b0302e4-4561-4aa8-a66a-0a4a8fa28049", "User", "USER" },
                    { "99ffe4af-e809-4c34-85f8-f8b6572901ab", "4361d154-4598-46d8-b191-519591a60cd9", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Confectionery" },
                    { 1, "Vegetables" },
                    { 4, "Meat" },
                    { 3, "Grocery" },
                    { 2, "Fruits" },
                    { 5, "Dairy Products" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "LocationURl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2245.0599144050398!2d37.633201316046446!3d55.75746139909995!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46b54bc6463a7e1f%3A0xd68e89e31b1d749d!2sUnderdog!5e0!3m2!1sru!2sby!4v1629892468792!5m2!1sru!2sby", "Underdog" },
                    { 2, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24955.706297501256!2d27.025704972171763!3d38.56917789967588!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd0550cc74b71%3A0x1187e14414fd380b!2sAtha%20Pack!5e0!3m2!1sru!2sby!4v1629892445596!5m2!1sru!2sby", "Atha Makina" },
                    { 4, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2217.634110197547!2d43.877038416060536!3d56.23255746235498!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x414e2b0cf172c9f5%3A0x52c68ec4e6912c0c!2z0JzQvtC70L7Rh9C90LDRjyDRgNC10LrQsA!5e0!3m2!1sru!2sby!4v1629892401840!5m2!1sru!2sby", "Milk Gorki" },
                    { 5, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d329288.70255430153!2d43.46470878878752!3d49.8505744089535!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4110ae4940ccb62b%3A0xbcc910b5c1d192f1!2z0JDRgNGH0LXQtNCw!5e0!3m2!1sru!2sby!4v1629892371206!5m2!1sru!2sby", "Archeda" },
                    { 6, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1514194.156913194!2d-8.354596788586328!3d42.16290088735141!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2585ac9f614b59%3A0x5fa1a18a4537ab19!2sLeche%20Pascual!5e0!3m2!1sru!2sby!4v1629892347122!5m2!1sru!2sby", "Pascual" },
                    { 7, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2940.5620697896807!2d-8.779720084303564!3d42.5221136328442!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2f6b3162dc2bb7%3A0x7393d35e4abdf3fb!2sAlimentos%20Javimar%20S.L.!5e0!3m2!1sru!2sby!4v1629892308672!5m2!1sru!2sby", "Javimar" },
                    { 8, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d11191.06220539455!2d25.314657263806758!3d53.86720882934107!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46de8adedfc9ddfd%3A0x2120bcee12937ed1!2z0JvQuNC00YHQutC40Lkg0LzQvtC70L7Rh9C90L4t0LrQvtC90YHQtdGA0LLQvdGL0Lkg0LrQvtC80LHQuNC90LDRgg!5e0!3m2!1sru!2sby!4v1629891953077!5m2!1sru!2sby", "MiLida" },
                    { 3, "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3023.4065496478047!2d46.268293615655864!3d40.73107894438454!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x403f5703b931bbad%3A0x6961e67ff6f0fa5b!2zU0jEsFLEsE4gQUdSTw!5e0!3m2!1sru!2sby!4v1629892427348!5m2!1sru!2sby", "Shirin Agro" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Cost", "Description", "ImageUrl", "Name", "ProviderId" },
                values: new object[,]
                {
                    { 5, 4, 3.8500000000000001, "Fresh", "https://i.pinimg.com/564x/99/ee/3c/99ee3cc80018401e8f92a794ce4d5102.jpg", "Sausage", 1 },
                    { 24, 1, 0.75, "Good for vision", "https://i.pinimg.com/564x/33/1b/e5/331be57b94b67ec8e145aafb382fe7a7.jpg", "Carrot", 5 },
                    { 26, 2, 2.6299999999999999, "Like a bruise", "https://i.pinimg.com/564x/41/7e/b1/417eb1212f7a3713aec76a166f3cfd81.jpg", "Plum", 5 },
                    { 31, 2, 1.49, "From my garden", "https://i.pinimg.com/564x/e3/7f/54/e37f547f5cbfc397a7caf53a4fe87b51.jpg", "Raspberry", 5 },
                    { 33, 3, 1.2, "Student food", "https://i.pinimg.com/564x/b3/76/3f/b3763f5a0de60415d3fcfe6c741a15c0.jpg", "Cereals", 5 },
                    { 7, 1, 1.1299999999999999, "Only from the garden", "https://i.pinimg.com/564x/6d/b9/cc/6db9cc5ed7fc8ee8492dfb60b50cbf28.jpg", "Cabbage", 6 },
                    { 8, 1, 1.0700000000000001, "Purple as a bruise", "https://i.pinimg.com/564x/98/3e/dd/983edd484bec3c19bbec13bf95ffb2c0.jpg", "Beetroot", 6 },
                    { 32, 2, 0.98999999999999999, "Sour like cranberries", "https://i.pinimg.com/564x/a5/58/fd/a558fdacb9b77ae1236dd0c24ae98c1c.jpg", "Lemon", 6 },
                    { 4, 6, 4.5, "Baked with love", "https://i.pinimg.com/564x/ec/5f/08/ec5f08f5c24077ba5a892e39105cc066.jpg", "Cake", 5 },
                    { 35, 2, 1.8899999999999999, "With a leaf", "https://i.pinimg.com/564x/37/f1/ba/37f1ba10f0fd4687f136d7e8288ed8a0.jpg", "Apricot", 6 },
                    { 12, 2, 2.4199999999999999, "Sugar taste", "https://i.pinimg.com/564x/c5/10/6f/c5106f5fd0684b45f12c8517a47cff5c.jpg", "Melon", 7 },
                    { 13, 2, 1.72, "Like an orange sunset", "https://i.pinimg.com/564x/e8/7a/07/e87a07cbdd1cc7d3638613e68db39a79.jpg", "Orange", 7 },
                    { 25, 6, 1.05, "In the shape of a fish", "https://i.pinimg.com/564x/61/64/99/616499b8d8bf55455880e89bc4eeb475.jpg", "Cookies", 7 },
                    { 23, 2, 2.1000000000000001, "Sour but expensive", "https://i.pinimg.com/564x/9c/cb/90/9ccb90064100431e87f0b593d31e21b3.jpg", "Cranberry", 8 },
                    { 28, 1, 2.6000000000000001, "On Halloween", "https://i.pinimg.com/564x/6f/bb/a7/6fbba70bc454a584a088386c23600d09.jpg", "Pumpkin", 8 },
                    { 29, 2, 1.5900000000000001, "Which fell on newton", "https://i.pinimg.com/564x/89/88/91/89889125af33bf6483f58f4fa4d3d9ea.jpg", "Apple", 8 },
                    { 37, 3, 5.6900000000000004, "Delicious with stew", "https://i.pinimg.com/564x/7d/76/de/7d76de4679c2cd3992b24c796f821c40.jpg", "Buckwheat", 8 },
                    { 40, 6, 4.9500000000000002, "In the form of a heart", "https://i.pinimg.com/564x/7e/31/1b/7e311b77e991407b30c5c4201fd76a40.jpg", "Pie", 6 },
                    { 22, 5, 4.3200000000000003, "Ratatouille near", "https://i.pinimg.com/564x/f4/54/c3/f454c30262fdbf92b128415b66f92794.jpg", "Cheese", 4 },
                    { 21, 5, 3.1200000000000001, "For cakes", "https://i.pinimg.com/564x/f4/30/7f/f4307f509a13125c657e882ae7ebb26a.jpg", "Cream", 4 },
                    { 2, 5, 2.25, "From the freshest milk", "https://i.pinimg.com/564x/d1/4a/ac/d14aac685c7d492d34d5b1c06f9e57ad.jpg", "Butter", 4 },
                    { 6, 4, 2.5499999999999998, "The most delicious meat", "https://i.pinimg.com/564x/11/f3/03/11f303bbb87435d297b257ffeaee3f1e.jpg", "Meatballs", 1 },
                    { 15, 4, 5.4900000000000002, "He can speak", "https://i.pinimg.com/564x/96/01/b3/9601b3603412852ef8fb8ccb940323ce.jpg", "Lamb", 1 },
                    { 16, 4, 4.0, "He was friends with a lamb", "https://i.pinimg.com/564x/15/0e/6b/150e6b8ac28a4ddbdc4640a9225b0712.jpg", "Veal", 1 },
                    { 27, 4, 5.0999999999999996, "Donald", "https://i.pinimg.com/564x/5a/90/ea/5a90eae4ae481f2d56f7fdd00110e85d.jpg", "Duck", 1 },
                    { 30, 6, 0.45000000000000001, "From Belarus", "https://i.pinimg.com/564x/22/c2/a3/22c2a39a04e3a978997f6bbfceeb4b2c.jpg", "Bread", 1 },
                    { 36, 4, 7.0999999999999996, "Not for vegans", "https://i.pinimg.com/564x/7f/cb/fa/7fcbfab68d2148614fc39bee18a1fd55.jpg", "Beef", 1 },
                    { 3, 5, 1.1699999999999999, "From the healthiest cows", "https://i.pinimg.com/564x/11/37/9a/11379a0588f57f9e18f9e4fae9f3b6ed.jpg", "Milk", 2 },
                    { 9, 1, 1.78, "At a discount", "https://i.pinimg.com/564x/47/a8/ed/47a8edca3321c8282a6cd2af73f1400a.jpg", "Asparagus", 2 },
                    { 10, 2, 1.1000000000000001, "Minions love them", "https://i.pinimg.com/564x/9c/e9/d5/9ce9d5b1f6c97f27d7b4f5a96d5ee6ab.jpg", "Banana", 2 },
                    { 11, 2, 1.99, "Hairy", "https://i.pinimg.com/564x/15/b4/99/15b499819be25fa974752cce150c19c6.jpg", "Kivifruit", 2 },
                    { 17, 4, 3.3500000000000001, "Of today's production", "https://i.pinimg.com/564x/54/30/14/543014bffb4aab8bfd5b3c169e7de841.jpg", "Chop", 2 },
                    { 18, 4, 3.1000000000000001, "Out of the oven", "https://i.pinimg.com/564x/dd/c4/2e/ddc42ece75ab1489285b6c8b1f21773b.jpg", "Chicken", 2 },
                    { 14, 2, 3.1000000000000001, "Avocado colors", "https://i.pinimg.com/564x/ce/a8/f1/cea8f12d0dc21d5ba8ffa9af50ee8b47.jpg", "Avocado", 3 },
                    { 19, 6, 0.80000000000000004, "For tea", "https://i.pinimg.com/564x/73/cc/1d/73cc1d67358e6297f3eccbc797fa449b.jpg", "Biscuit", 3 },
                    { 20, 1, 1.2, "From Belarusian fields", "https://i.pinimg.com/564x/da/6a/cf/da6acf414f983a50e0f836f2eabf43e7.jpg", "Potato", 3 },
                    { 34, 6, 0.80000000000000004, "For coffee", "https://i.pinimg.com/564x/ac/21/b0/ac21b0a905b1be5754a31bd82d1d89c4.jpg", "Wafer", 3 },
                    { 1, 5, 1.53, "Contains useful trace elements", "https://i.pinimg.com/564x/9f/19/09/9f19090f916c43dae8fa2d5e4f4298bd.jpg", "Yoghurt", 4 },
                    { 38, 3, 1.1799999999999999, "Chinese delicacy", "https://i.pinimg.com/564x/a4/5b/00/a45b00ba09ce546c6a3843b413addbdc.jpg", "Rice", 8 },
                    { 39, 3, 0.94999999999999996, "Ser", "https://i.pinimg.com/564x/b1/8c/ba/b18cba5f27ab4c7a2ec6dc8c117da2c2.jpg", "Oatmeal", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId_ProductId",
                table: "Carts",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name_Description_Cost_CategoryId_ProviderId",
                table: "Products",
                columns: new[] { "Name", "Description", "Cost", "CategoryId", "ProviderId" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Description] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProviderId",
                table: "Products",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_Name",
                table: "Providers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
