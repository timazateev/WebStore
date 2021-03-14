using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", Age = 22 },
            new Employee { id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", Age = 21 },
            new Employee { id = 3, LastName = "Timov", FirstName = "Tim", Patronymic = "Timovich", Age = 32 },
        };
        public IActionResult Index() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

        public IActionResult Employees()
        {
            return View(__Employees);
        }
    }
}
