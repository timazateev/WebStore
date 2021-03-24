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
            new Section {id = 6, Name = "For Mans Shorts", Order = 1, ParentId = 5 },
            new Section {id = 7, Name = "For Mans Shoes", Order = 0, ParentId =5 }
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand {id = 1, Name = "Acne", Order = 0  },
            new Brand {id = 2, Name = "Grune Erde", Order = 1  },
            new Brand {id = 3, Name = "Aaaa1", Order = 2  }
        };
    }
}
