using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{   
    //[Route("Staff")]
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _Employees;
        public EmployeesController() 
        {
            _Employees = TestData.Employees;
        }

        //[Route("All")]
        public IActionResult Index() => View(_Employees);

        //[Route("info-(id-{id})")]  // cheange address for employee
        public IActionResult Details(int id)
        {
            var empluyee = _Employees.FirstOrDefault(e => e.id ==id);
            if (empluyee is null)
                return NotFound();

            return View(empluyee);
        }
    }
}
