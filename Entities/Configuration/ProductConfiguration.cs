using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product
                {
                    Id = 1,
                    Name = "Yoghurt",
                    Description = "Contains useful trace elements",
                    Cost = 1.53,
                    CategoryId = 5,
                    ProviderId = 4,
                },
                new Product
                {
                    Id = 2,
                    Name = "Butter",
                    Description = "From the freshest milk",
                    Cost = 2.25,
                    CategoryId = 5,
                    ProviderId = 4,
                },
                new Product
                {
                    Id = 3,
                    Name = "Milk",
                    Description = "From the healthiest cows",
                    Cost = 1.17,
                    CategoryId = 5,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 4,
                    Name = "Cake",
                    Description = "Baked with love",
                    Cost = 4.5,
                    CategoryId = 6,
                    ProviderId = 5,
                },
                new Product
                {
                    Id = 5,
                    Name = "Sausage",
                    Description = "Fresh",
                    Cost = 3.85,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 6,
                    Name = "Meatballs",
                    Description = "The most delicious meat",
                    Cost = 2.55,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 7,
                    Name = "Cabbage",
                    Description = "Only from the garden",
                    Cost = 1.13,
                    CategoryId = 1,
                    ProviderId = 6,
                },
                new Product
                {
                    Id = 8,
                    Name = "Beetroot",
                    Description = "Purple as a bruise",
                    Cost = 1.07,
                    CategoryId = 1,
                    ProviderId = 6,
                },
                new Product
                {
                    Id = 9,
                    Name = "Asparagus",
                    Description = "At a discount",
                    Cost = 1.78,
                    CategoryId = 1,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 10,
                    Name = "Banana",
                    Description = "Minions love them",
                    Cost = 1.10,
                    CategoryId = 2,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 11,
                    Name = "Kivifruit",
                    Description = "Hairy",
                    Cost = 1.99,
                    CategoryId = 2,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 12,
                    Name = "Melon",
                    Description = "Sugar taste",
                    Cost = 2.42,
                    CategoryId = 2,
                    ProviderId = 7,
                },
                new Product
                {
                    Id = 13,
                    Name = "Orange",
                    Description = "Like an orange sunset",
                    Cost = 1.72,
                    CategoryId = 2,
                    ProviderId = 7,
                },
                new Product
                {
                    Id = 14,
                    Name = "Avocado",
                    Description = "Avocado colors",
                    Cost = 3.10,
                    CategoryId = 2,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 15,
                    Name = "Lamb",
                    Description = "He can speak",
                    Cost = 5.49,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 16,
                    Name = "Veal",
                    Description = "He was friends with a lamb",
                    Cost = 4.00,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 17,
                    Name = "Chop",
                    Description = "Of today's production",
                    Cost = 3.35,
                    CategoryId = 4,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 18,
                    Name = "Chicken",
                    Description = "Out of the oven",
                    Cost = 3.10,
                    CategoryId = 4,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 19,
                    Name = "Biscuit",
                    Description = "For tea",
                    Cost = 0.80,
                    CategoryId = 6,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 20,
                    Name = "Potato",
                    Description = "From Belarusian fields",
                    Cost = 1.20,
                    CategoryId = 1,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 21,
                    Name = "Cream",
                    Description = "For cakes",
                    Cost = 3.12,
                    CategoryId = 5,
                    ProviderId = 4,
                },
                new Product
                {
                    Id = 22,
                    Name = "Cheese",
                    Description = "Ratatouille near",
                    Cost = 4.32,
                    CategoryId = 5,
                    ProviderId = 4,
                },
                new Product
                {
                    Id = 23,
                    Name = "Cranberry",
                    Description = "Sour but expensive",
                    Cost = 2.10,
                    CategoryId = 2,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 24,
                    Name = "Carrot",
                    Description = "Good for vision",
                    Cost = 0.75,
                    CategoryId = 1,
                    ProviderId = 5,
                },
                new Product
                {
                    Id = 25,
                    Name = "Cookies",
                    Description = "In the shape of a fish",
                    Cost = 1.05,
                    CategoryId = 6,
                    ProviderId = 7,
                },
                new Product
                {
                    Id = 26,
                    Name = "Plum",
                    Description = "Like a bruise",
                    Cost = 2.63,
                    CategoryId = 2,
                    ProviderId = 5,
                },
                new Product
                {
                    Id = 27,
                    Name = "Duck",
                    Description = "Donald",
                    Cost = 5.10,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 28,
                    Name = "Pumpkin",
                    Description = "On Halloween",
                    Cost = 2.60,
                    CategoryId = 1,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 29,
                    Name = "Apple",
                    Description = "Which fell on newton",
                    Cost = 1.59,
                    CategoryId = 2,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 30,
                    Name = "Bread",
                    Description = "From Belarus",
                    Cost = 0.45,
                    CategoryId = 6,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 31,
                    Name = "Raspberry",
                    Description = "From my garden",
                    Cost = 1.49,
                    CategoryId = 2,
                    ProviderId = 5,
                },
                new Product
                {
                    Id = 32,
                    Name = "Lemon",
                    Description = "Sour like cranberries",
                    Cost = 0.99,
                    CategoryId = 2,
                    ProviderId = 6,
                },
                new Product
                {
                    Id = 33,
                    Name = "Cereals",
                    Description = "Student food",
                    Cost = 1.20,
                    CategoryId = 3,
                    ProviderId = 5,
                },
                new Product
                {
                    Id = 34,
                    Name = "Wafer",
                    Description = "For coffee",
                    Cost = 0.80,
                    CategoryId = 6,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 35,
                    Name = "Apricot",
                    Description = "With a leaf",
                    Cost = 1.89,
                    CategoryId = 2,
                    ProviderId = 6,
                },
                new Product
                {
                    Id = 36,
                    Name = "Beef",
                    Description = "Not for vegans",
                    Cost = 7.10,
                    CategoryId = 4,
                    ProviderId = 1,
                },
                new Product
                {
                    Id = 37,
                    Name = "Buckwheat",
                    Description = "Delicious with stew",
                    Cost = 5.69,
                    CategoryId = 3,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 38,
                    Name = "Rice",
                    Description = "Chinese delicacy",
                    Cost = 1.18,
                    CategoryId = 3,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 39,
                    Name = "Oatmeal",
                    Description = "Ser",
                    Cost = 0.95,
                    CategoryId = 3,
                    ProviderId = 8,
                },
                new Product
                {
                    Id = 40,
                    Name = "Pie",
                    Description = "In the form of a heart",
                    Cost = 4.95,
                    CategoryId = 6,
                    ProviderId = 6,
                }
            );
        }
    }
}
