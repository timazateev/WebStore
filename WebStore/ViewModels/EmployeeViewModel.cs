using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        [Range(18, 80, ErrorMessage = "Age should be between 18 and 80")]
        public int Age { get; set; }
    }
}
