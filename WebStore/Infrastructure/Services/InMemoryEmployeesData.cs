using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees;
        private int _CurrentMaxId;
        public InMemoryEmployeesData()
        {
            _Employees = TestData.Employees;
            _CurrentMaxId = _Employees.DefaultIfEmpty().Max(e => e?.id ?? 1);
        }
        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return employee.id; // remove after DB migration

            employee.id = ++_CurrentMaxId; // not for multithreading
            _Employees.Add(employee);
            return employee.id;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null) return false;
            return _Employees.Remove(db_item);
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee Get(int id) => _Employees.FirstOrDefault(e => e.id == id);

        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
            _Employees.FirstOrDefault(e => e.LastName == LastName && e.FirstName == FirstName && e.Patronymic == Patronymic);

        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            var employee = new Employee
            {
                LastName = LastName,
                FirstName = FirstName,
                Patronymic = Patronymic,
                Age = Age
            };
            Add(employee);
            return employee;

        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return; //remove after DB migration

            var db_item = Get(employee.id);
            if (db_item is null) return;

            //id not for copying because of DB
            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;

            // SaveChanges(); // in case of DB

        }
    }
}
