using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Models;

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
    }
}
