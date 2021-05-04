using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Interfaces;
using WebStore.Models;

namespace WebStore.Clients.Employees
{
    class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebAPI.Employees) { }
        {
public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            throw new NotImplementedException();
        }

        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetByName(string LastName, string FirstName, string Patronymic)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
    }
}
