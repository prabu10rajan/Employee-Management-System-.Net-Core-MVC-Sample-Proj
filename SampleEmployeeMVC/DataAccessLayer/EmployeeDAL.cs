using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using SampleEmployeeMVC.Models;

namespace SampleEmployeeMVC.DataAccessLayer
{
    

    public class EmployeeDAL
    {
        private SqlConnection connection;

        /// <summary>
        /// In .Net Core, Connection string is brought from json file as apposed to Web Config in ASP .Net, as web config could 
        /// be understood only by IIS Manager. This part is mostly done in a separate CS file and initialized in controller.
        /// </summary>
        public EmployeeDAL()
        {
            var conn = GetConfiguration();
            connection = new SqlConnection(conn.GetSection("ConnectionStrings").GetSection("DefaultSqlServerConnection").Value);
        }

        /// <summary>
        /// If different SqlConnection object is passed from Controller.
        /// </summary>
        /// <param name="sqlConnection">passing sqlConnection.</param>
        public EmployeeDAL(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        /// <summary>
        /// Building Configuration settings from Json file.
        /// </summary>
        /// <returns>returns IConfiguration object</returns>
        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        /// <summary>
        /// To Create new Employee record.
        /// </summary>
        /// <param name="employee">passing employee</param>
        /// <returns>returns result string.</returns>
        public string CreateEmployee(Employee employee)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("USP_CRUDOperations_Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", 0);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Mobile", employee.Mobile);
                cmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Operation", "Create");
                connection.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while(sqlDataReader.Read())
                    {
                        result = sqlDataReader["Result"].ToString();
                    }
                }

                return result;
            }
            catch
            {
                return result = "Error in Inserting Employee Record!";
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// To update a single employee record.
        /// </summary>
        /// <param name="employee">passing employee object</param>
        /// <returns>returns result string.</returns>
        public string UpdateEmployee(Employee employee)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("USP_CRUDOperations_Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Mobile", employee.Mobile);
                cmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Operation", "Update");
                connection.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        result = sqlDataReader["Result"].ToString();
                    }
                }

                return result;
            }
            catch
            {
                return result = "Error in Updating Employee Record!";
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// To delete a single employee record.
        /// </summary>
        /// <param name="employee">passing employee object</param>
        /// <returns>returns result string.</returns>
        public string DeleteEmployee(Employee employee)
        {
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("USP_CRUDOperations_Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Mobile", employee.Mobile);
                cmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Operation", "Delete");
                connection.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        result = Convert.ToString(sqlDataReader["Result"]);
                    }
                }
                return result;
            }
            catch
            {
                return result = "Error in Deleting Employee Record!";
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// To retrive a single employee record by ID.
        /// </summary>
        /// <param name="employee">passing employee id</param>
        /// <returns>returns employee object.</returns>
        public Employee GetEmployeeByID(int EmployeeID)
        {
            Employee emp = new Employee();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_CRUDOperations_Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", EmployeeID);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@Mobile", "");
                cmd.Parameters.AddWithValue("@EmailID", "");
                cmd.Parameters.AddWithValue("@Department", "");
                cmd.Parameters.AddWithValue("@Salary", 0);
                cmd.Parameters.AddWithValue("@Operation", "Select");
                connection.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        emp.EmployeeID = Convert.ToInt32(sqlDataReader["EmpID"]);
                        emp.Name = Convert.ToString(sqlDataReader["Name"]);
                        emp.Address = Convert.ToString(sqlDataReader["Address"]);
                        emp.Mobile = Convert.ToString(sqlDataReader["Mobile"]);
                        emp.EmailID = Convert.ToString(sqlDataReader["EmailID"]);
                        emp.Department = Convert.ToString(sqlDataReader["Department"]);
                        emp.Salary = Convert.ToInt32(sqlDataReader["Salary"]);
                    }
                }

                return emp;
            }
            catch
            {
                emp.ErrorMsg = "Error in Selecting Employee Record!";
                return emp;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// To retrive list of all employee records.
        /// </summary>
        /// <returns>returns list of employee objects.</returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_CRUDOperations_Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", 0);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@Mobile", "");
                cmd.Parameters.AddWithValue("@EmailID", "");
                cmd.Parameters.AddWithValue("@Department", "");
                cmd.Parameters.AddWithValue("@Salary", 0);
                cmd.Parameters.AddWithValue("@Operation", "SelectAll");
                connection.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeID = Convert.ToInt32(sqlDataReader["EmpID"]);
                        emp.Name = Convert.ToString(sqlDataReader["Name"]);
                        emp.Address = Convert.ToString(sqlDataReader["Address"]);
                        emp.Mobile = Convert.ToString(sqlDataReader["Mobile"]);
                        emp.EmailID = Convert.ToString(sqlDataReader["EmailID"]);
                        emp.Department = Convert.ToString(sqlDataReader["Department"]);
                        emp.Salary = Convert.ToInt32(sqlDataReader["Salary"]);

                        empList.Add(emp);
                    }
                }

                return empList;
            }
            catch
            {
                return empList;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
