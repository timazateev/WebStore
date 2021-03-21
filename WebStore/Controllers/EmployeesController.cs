using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Staff")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        //private readonly List<Employee> _Employees;  //obsolete workflow

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
            //_Employees = TestData.Employees; //obsolete workflow

        }

        //[Route("All")]
        public IActionResult Index() => View(_EmployeesData.Get());

        //[Route("info-(id-{id})")]  // cheange address for employee
        public IActionResult Details(int id)
        {
            var empluyee = _EmployeesData.Get(id);
            if (empluyee is null)
                return NotFound();

            return View(empluyee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _EmployeesData.Get(id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                id = employee.id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });

        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model) 
        {
            var employee = new Employee
            {
                id = model.id,
                LastName = model.LastName,
                FirstName = model.Name,
                Patronymic = model.Patronymic,
                Age = model.Age
            };

            _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }


    }
}
