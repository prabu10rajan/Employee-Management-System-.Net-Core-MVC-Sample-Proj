using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleEmployeeMVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Enter Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Employee's Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Employee's Mobileno")]
        [MaxLength(10, ErrorMessage ="Invalid Mobile number!")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Enter Employee's EmailID")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Enter Employee's Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Enter Employee's Salary")]
        [RegularExpression("([0-9]+)", ErrorMessage ="Enter numbers for salary!")]
        public int Salary { get; set; }

        public List<Employee> AllEmployees { get; set; }

        public string ErrorMsg { get; set; }
    }
}
