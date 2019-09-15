using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleEmployeeMVC.DataAccessLayer;
using SampleEmployeeMVC.Models;

namespace SampleEmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {

        public IActionResult Home()
        {
            EmployeeDAL dal = new EmployeeDAL();
            Employee emp = new Employee();
            try
            {
                emp.AllEmployees = dal.GetEmployees();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return this.View(emp);
        }

        [HttpGet]
        public ActionResult ReadEmployee(int employeeID)
        {
            EmployeeDAL dal = new EmployeeDAL();
            return this.View(dal.GetEmployeeByID(employeeID));
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDAL dal = new EmployeeDAL();
                    string result = dal.CreateEmployee(employee);
                    this.TempData["Result"] = result;
                    ModelState.Clear();
                }
            }

            catch
            {
                ModelState.AddModelError("", "Error in inserting data");
            }
            
            return this.View();
        }

        [HttpGet]
        public ActionResult UpdateEmployee(int employeeID)
        {
            EmployeeDAL dal = new EmployeeDAL();
            return this.View(dal.GetEmployeeByID(employeeID));
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDAL dal = new EmployeeDAL();
                    string result = dal.UpdateEmployee(employee);
                    TempData["Result"] = result;
                    ModelState.Clear();
                }
            }

            catch
            {
                ModelState.AddModelError("", "Error in inserting data");
            }

            return this.View();
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int employeeID)
        {
            EmployeeDAL dal = new EmployeeDAL();
            return this.View(dal.GetEmployeeByID(employeeID));
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Employee employee)
        {
            try
            {
                EmployeeDAL dal = new EmployeeDAL();
                string result = dal.DeleteEmployee(employee);
                this.TempData["Result"] = result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return this.View();
        }
    }
}