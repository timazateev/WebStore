using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            Get<Employee>($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}");

        public int Add(Employee employee) => Post(Address, employee).Content.ReadAsAsync<int>().Result;
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) =>
            Post($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}", "")
            .Content.ReadAsAsync<Employee>().Result;

        public void Update(Employee employee) => Put(Address, employee);

        public bool Delete(int id) => Delete($"{Address}/{id}").IsSuccessStatusCode;

    }
}
