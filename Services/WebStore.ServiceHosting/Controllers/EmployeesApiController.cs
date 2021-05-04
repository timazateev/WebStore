using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : Controller, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(
            IEmployeesData EmployeesData,
            ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpGet] // http://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();
        
        [HttpGet("{id}")] //http://localhost:5001/api/employees/5
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpGet("employee")] //http://localhost:5001/api/employees/employee?LastName=Ivanov&FifstName=Ivan&Patronymic=Ivanovich
        public Employee GetByName(string LastName, string FirstName, string Patronymic) => _EmployeesData.GetByName(LastName, FirstName, Patronymic);

        [HttpPost]
        public int Add(Employee employee)
        {
            _Logger.LogInformation("Employee adding {0}", employee);
            return _EmployeesData.Add(employee);
        }

        [HttpPost("employee")] //post -> http://localhost:5001/api/employees/employee?LastName=Ivanov&FifstName=Ivan&Patronymic=Ivanovich&Age=33
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            _Logger.LogInformation("Employee adding {0} {1} {2} {3} age",
                LastName, FirstName, Patronymic, Age);
            return _EmployeesData.Add(LastName, FirstName, Patronymic, Age);
        }

        //[HttpPut("{id}")] // put -> http://localhost:5001/api/employees/5
        [HttpPut] // put -> http://localhost:5001/api/employees/5
        public void Update(Employee employee)
        {
            _Logger.LogInformation("Employee editing {0}", employee);
            _EmployeesData.Update(employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _Logger.LogInformation("Employee deleting {0}", id);
            var result = _EmployeesData.Delete(id);
            _Logger.LogInformation("Employee deleting with id {0} - {1}", id, result ? "finished" : "not found");
            return result;
        }
    }
}
