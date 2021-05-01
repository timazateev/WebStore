using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Models;
using WebStore.Domain.ViewModels;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Controllers
{
    //[Route("Staff")]
    [Authorize]
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
        
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());
        
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.Get((int)id);

            if (employee is null)
                return NotFound();

            if (ModelState.IsValid)
            {

                return View(new EmployeeViewModel
                {
                    id = employee.id,
                    LastName = employee.LastName,
                    Name = employee.FirstName,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age
                });
            }
            else
                return View("Edit", new EmployeeViewModel());

        }

        [Authorize(Roles = Role.Administrators)]
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
            if (ModelState.IsValid) 
            {
                if (employee.id == 0)
                    _EmployeesData.Add(employee);
                else
                    _EmployeesData.Update(employee);
                return RedirectToAction("Index");
            }
            else
                return View(model);


        }

        [Authorize(Roles = Role.Administrators)]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

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

        [Authorize(Roles = Role.Administrators)]
        [HttpPost] //Post is significant here for security
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
