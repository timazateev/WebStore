using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStoreDomain.Entities;

namespace WebStore.Data
{
    internal static class TestData
    {
        public static List<Employee> Employees { get; set; } = new()
        {
            new Employee { id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", Age = 22 },
            new Employee { id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", Age = 21 },
            new Employee { id = 3, LastName = "Timov", FirstName = "Tim", Patronymic = "Timovich", Age = 32 },
        };

        public static IEnumerable<Section> Sections { get; } = new[]
        {
            new Section {id = 1, Name = "Sport", Order = 0 },
            new Section {id = 2, Name = "Nike", Order = 0, ParentId = 1 },
            new Section {id = 3, Name = "Under Armor", Order = 1, ParentId = 1 },
            new Section {id = 4, Name = "Test", Order = 2, ParentId = 1 },
            new Section {id = 5, Name = "For Mans", Order = 1 },
            new Section {id = 6, Name = "For Man Shorts", Order = 1, ParentId = 5 },
            new Section {id = 7, Name = "For Man Shoes", Order = 0, ParentId =5 },
            new Section {id = 8, Name = "For Woman Shoes", Order = 0},
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand {id = 1, Name = "Acne", Order = 0  },
            new Brand {id = 2, Name = "Grune Erde", Order = 1  },
            new Brand {id = 3, Name = "Aaaa1", Order = 2  },
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
        new Product() { id = 1, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product10.jpg", Order = 0, SectionId = 2, BrandId = 1},
        new Product() { id = 1, Name = "Some stort item", Price = 250, ImageUrl = "product11.jpg", Order = 1, SectionId = 2, BrandId = 1},
        new Product() { id = 1, Name = "Dress item", Price = 3025, ImageUrl = "product12.jpg", Order = 2, SectionId = 2, BrandId = 2},
        new Product() { id = 1, Name = "Edition item", Price = 25, ImageUrl = "product7.jpg", Order = 3, SectionId = 2, BrandId = 2},
        new Product() { id = 1, Name = "product item 5", Price = 125, ImageUrl = "product9.jpg", Order = 4, SectionId = 2, BrandId = 2},
        };
    }
}
