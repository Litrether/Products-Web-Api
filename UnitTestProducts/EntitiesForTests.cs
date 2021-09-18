using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProducts
{
    public static class EntitiesForTests
    {
        public static IQueryable<Category> Categories =>
            new List<Category>()
            {
                new Category{Id = 1, Name = "Vegetables"},
                new Category{Id = 2, Name = "Fruits"},
                new Category{Id = 3, Name = "Grocery"},
                new Category{Id = 4, Name = "Meat"},
                new Category{Id = 5, Name = "Dairy Products"},
                new Category{Id = 6, Name = "Confectionery"}
            }.AsQueryable();

        public static IQueryable<Provider> Providers =>
            new List<Provider>()
            {
                new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                new Provider { Id = 3, Name = "Shirin Agro", LocationLat = 40.73105861912476m, LocationLong = 46.27047156919906m, Products = { }},
                new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                new Provider { Id = 5, Name = "Archeda", LocationLat = 49.764727469041816m, LocationLong = 43.65468679640968m, Products = { }},
                new Provider { Id = 6, Name = "Pascual", LocationLat = 47.46106041862809m, LocationLong = -122.26236529486663m, Products = { }},
                new Provider { Id = 7, Name = "Javimar", LocationLat = 39.16566430628417m, LocationLong = -0.2430474019997804m, Products = { }},
                new Provider { Id = 8, Name = "MiLida", LocationLat = 10.158678793639453m, LocationLong = -10.753070951045318m, Products = { }}
            }.AsQueryable();

        public static IQueryable<Product> Products =>
            new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Yoghurt",
                    Description = "Contains useful trace elements",
                    Cost = 1.53,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category{Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/9f/19/09/9f19090f916c43dae8fa2d5e4f4298bd.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Butter",
                    Description = "From the freshest milk",
                    Cost = 2.25,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category{ Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/d1/4a/ac/d14aac685c7d492d34d5b1c06f9e57ad.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Milk",
                    Description = "From the healthiest cows",
                    Cost = 1.17,
                    CategoryId = 5,
                    ProviderId = 2,
                    Category = new Category{ Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/11/37/9a/11379a0588f57f9e18f9e4fae9f3b6ed.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Cake",
                    Description = "Baked with love",
                    Cost = 4.5,
                    CategoryId = 6,
                    ProviderId = 5,
                    Category = new Category{ Id = 6, Name = "Confectionery"},
                    Provider = new Provider { Id = 5, Name = "Archeda", LocationLat = 49.764727469041816m, LocationLong = 43.65468679640968m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/ec/5f/08/ec5f08f5c24077ba5a892e39105cc066.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Sausage",
                    Description = "Fresh",
                    Cost = 3.85,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category{Id = 4, Name = "Meat"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/99/ee/3c/99ee3cc80018401e8f92a794ce4d5102.jpg"
                },new Product
                {
                    Id = 6,
                    Name = "Meatballs",
                    Description = "The most delicious meat",
                    Cost = 2.55,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category{Id = 4, Name = "Meat"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/11/f3/03/11f303bbb87435d297b257ffeaee3f1e.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "Cabbage",
                    Description = "Only from the garden",
                    Cost = 1.13,
                    CategoryId = 1,
                    ProviderId = 6,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 6, Name = "Pascual", LocationLat = 47.46106041862809m, LocationLong = -122.26236529486663m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/6d/b9/cc/6db9cc5ed7fc8ee8492dfb60b50cbf28.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "Beetroot",
                    Description = "Purple as a bruise",
                    Cost = 1.07,
                    CategoryId = 1,
                    ProviderId = 6,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 6, Name = "Pascual", LocationLat = 47.46106041862809m, LocationLong = -122.26236529486663m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/98/3e/dd/983edd484bec3c19bbec13bf95ffb2c0.jpg"
                },
                new Product
                {
                    Id = 9,
                    Name = "Asparagus",
                    Description = "At a discount",
                    Cost = 1.78,
                    CategoryId = 1,
                    ProviderId = 2,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/47/a8/ed/47a8edca3321c8282a6cd2af73f1400a.jpg"
                },
                new Product
                {
                    Id = 10,
                    Name = "Banana",
                    Description = "Minions love them",
                    Cost = 1.10,
                    CategoryId = 2,
                    ProviderId = 2,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/9c/e9/d5/9ce9d5b1f6c97f27d7b4f5a96d5ee6ab.jpg"
                },
                new Product
                {
                    Id = 11,
                    Name = "Kivifruit",
                    Description = "Hairy",
                    Cost = 1.99,
                    CategoryId = 2,
                    ProviderId = 2,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/15/b4/99/15b499819be25fa974752cce150c19c6.jpg"
                },
                new Product
                {
                    Id = 12,
                    Name = "Melon",
                    Description = "Sugar taste",
                    Cost = 2.42,
                    CategoryId = 2,
                    ProviderId = 7,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 7, Name = "Javimar", LocationLat = 39.16566430628417m, LocationLong = -0.2430474019997804m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/c5/10/6f/c5106f5fd0684b45f12c8517a47cff5c.jpg"
                },
                new Product
                {
                    Id = 13,
                    Name = "Orange",
                    Description = "Like an orange sunset",
                    Cost = 1.72,
                    CategoryId = 2,
                    ProviderId = 7,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 7, Name = "Javimar", LocationLat = 39.16566430628417m, LocationLong = -0.2430474019997804m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/e8/7a/07/e87a07cbdd1cc7d3638613e68db39a79.jpg"
                },
                new Product
                {
                    Id = 14,
                    Name = "Avocado",
                    Description = "Avocado colors",
                    Cost = 3.10,
                    CategoryId = 2,
                    ProviderId = 3,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 3, Name = "Shirin Agro", LocationLat = 40.73105861912476m, LocationLong = 46.27047156919906m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/ce/a8/f1/cea8f12d0dc21d5ba8ffa9af50ee8b47.jpg"
                },
                new Product
                {
                    Id = 15,
                    Name = "Lamb",
                    Description = "He can speak",
                    Cost = 5.49,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category{Id = 4, Name = "Meat"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/96/01/b3/9601b3603412852ef8fb8ccb940323ce.jpg"
                },
                new Product
                {
                    Id = 16,
                    Name = "Veal",
                    Description = "He was friends with a lamb",
                    Cost = 4.00,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category{Id = 4, Name = "Meat"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/15/0e/6b/150e6b8ac28a4ddbdc4640a9225b0712.jpg"
                },
                new Product
                {
                    Id = 17,
                    Name = "Chop",
                    Description = "Of today's production",
                    Cost = 3.35,
                    CategoryId = 4,
                    ProviderId = 2,
                    Category = new Category{Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/54/30/14/543014bffb4aab8bfd5b3c169e7de841.jpg"
                },
                new Product
                {
                    Id = 18,
                    Name = "Chicken",
                    Description = "Out of the oven",
                    Cost = 3.10,
                    CategoryId = 4,
                    ProviderId = 2,
                    Category = new Category{Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/dd/c4/2e/ddc42ece75ab1489285b6c8b1f21773b.jpg"
                },
                new Product
                {
                    Id = 19,
                    Name = "Biscuit",
                    Description = "For tea",
                    Cost = 0.80,
                    CategoryId = 6,
                    ProviderId = 3,
                    Category = new Category{ Id = 6, Name = "Confectionery"},
                    Provider = new Provider { Id = 3, Name = "Shirin Agro", LocationLat = 40.73105861912476m, LocationLong = 46.27047156919906m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/73/cc/1d/73cc1d67358e6297f3eccbc797fa449b.jpg"
                },
                new Product
                {
                    Id = 20,
                    Name = "Potato",
                    Description = "From Belarusian fields",
                    Cost = 1.20,
                    CategoryId = 1,
                    ProviderId = 3,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 3, Name = "Shirin Agro", LocationLat = 40.73105861912476m, LocationLong = 46.27047156919906m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/da/6a/cf/da6acf414f983a50e0f836f2eabf43e7.jpg"
                },
                new Product
                {
                    Id = 21,
                    Name = "Cream",
                    Description = "For cakes",
                    Cost = 3.12,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category{Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/f4/30/7f/f4307f509a13125c657e882ae7ebb26a.jpg"
                },
                new Product
                {
                    Id = 22,
                    Name = "Cheese",
                    Description = "Ratatouille near",
                    Cost = 4.32,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category{Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/f4/54/c3/f454c30262fdbf92b128415b66f92794.jpg"
                },
                new Product
                {
                    Id = 23,
                    Name = "Cranberry",
                    Description = "Sour but expensive",
                    Cost = 2.10,
                    CategoryId = 2,
                    ProviderId = 8,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 8, Name = "MiLida", LocationLat = 10.158678793639453m, LocationLong = -10.753070951045318m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/9c/cb/90/9ccb90064100431e87f0b593d31e21b3.jpg"
                },
                new Product
                {
                    Id = 24,
                    Name = "Carrot",
                    Description = "Good for vision",
                    Cost = 0.75,
                    CategoryId = 1,
                    ProviderId = 5,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 5, Name = "Archeda", LocationLat = 49.764727469041816m, LocationLong = 43.65468679640968m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/33/1b/e5/331be57b94b67ec8e145aafb382fe7a7.jpg"
                },
                new Product
                {
                    Id = 25,
                    Name = "Cookies",
                    Description = "In the shape of a fish",
                    Cost = 1.05,
                    CategoryId = 6,
                    ProviderId = 7,
                    Category = new Category{Id = 6, Name = "Confectionery"},
                    Provider = new Provider { Id = 7, Name = "Javimar", LocationLat = 39.16566430628417m, LocationLong = -0.2430474019997804m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/61/64/99/616499b8d8bf55455880e89bc4eeb475.jpg"
                },
                new Product
                {
                    Id = 26,
                    Name = "Plum",
                    Description = "Like a bruise",
                    Cost = 2.63,
                    CategoryId = 2,
                    ProviderId = 5,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 5, Name = "Archeda", LocationLat = 49.764727469041816m, LocationLong = 43.65468679640968m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/41/7e/b1/417eb1212f7a3713aec76a166f3cfd81.jpg"
                },
                new Product
                {
                    Id = 27,
                    Name = "Duck",
                    Description = "Donald",
                    Cost = 5.10,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category{Id = 4, Name = "Meat"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/5a/90/ea/5a90eae4ae481f2d56f7fdd00110e85d.jpg"
                },
                new Product
                {
                    Id = 28,
                    Name = "Pumpkin",
                    Description = "On Halloween",
                    Cost = 2.60,
                    CategoryId = 1,
                    ProviderId = 8,
                    Category = new Category{Id = 1, Name = "Vegetables"},
                    Provider = new Provider { Id = 8, Name = "MiLida", LocationLat = 10.158678793639453m, LocationLong = -10.753070951045318m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/6f/bb/a7/6fbba70bc454a584a088386c23600d09.jpg"
                },
                new Product
                {
                    Id = 29,
                    Name = "Apple",
                    Description = "Which fell on newton",
                    Cost = 1.59,
                    CategoryId = 2,
                    ProviderId = 8,
                    Category = new Category{Id = 2, Name = "Fruits"},
                    Provider = new Provider { Id = 8, Name = "MiLida", LocationLat = 10.158678793639453m, LocationLong = -10.753070951045318m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/89/88/91/89889125af33bf6483f58f4fa4d3d9ea.jpg"
                },
                new Product
                {
                    Id = 30,
                    Name = "Bread",
                    Description = "From Belarus",
                    Cost = 0.45,
                    CategoryId = 6,
                    ProviderId = 1,
                    Category = new Category{ Id = 6, Name = "Confectionery"},
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/22/c2/a3/22c2a39a04e3a978997f6bbfceeb4b2c.jpg"
                },
            }.AsQueryable();
    }
}
