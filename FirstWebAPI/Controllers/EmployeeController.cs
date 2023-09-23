using FirstWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private RepositoryEmployee _repositoryEmployee;
        public EmployeeController(RepositoryEmployee repository)
        {
            _repositoryEmployee = repository;
        }
        [HttpGet]
        // GET: EmployeeController
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = _repositoryEmployee.GetAllEmployees();
            return employees;
        }

        [HttpGet("/GetEmployees")]
        public IEnumerable<EmployeeViewModel> GetAllEmployees()
        {
            List<Employee> employee = _repositoryEmployee.GetAllEmployees();
            var empList = 
                (
                from emp in employee
                select new EmployeeViewModel()
                {
                    EmpId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    BirthDate = emp.BirthDate,
                    HireDate = emp.HireDate,
                    Title = emp.Title,
                    City = emp.City,
                    ReportsTo = emp.ReportsTo
                }
                ).ToList();
            return empList;
        }

        [HttpPost]
        public Employee EmployeeDetails(int id)
        {
            //Customer customer = _repositoryCustomers.FindCustomerById(id);
            //return View(customer);
            Employee employees = _repositoryEmployee.FindEmployeeById(id);
            return employees;
        }
        [HttpPost("/AddNewEmployee")]
        public int  AddNewEmployee([FromBody]Employee employee) 
        {
            _repositoryEmployee.AddNewEmployee(employee);
            return 1;
        }
        [HttpPut("/EditEmployee")]
        public void EditEmployee([FromBody] EmployeeViewModel updatedEmployee)
        {
            Employee employee = new Employee() 
            {
                EmployeeId = updatedEmployee.EmpId, FirstName = updatedEmployee.FirstName, LastName = updatedEmployee.LastName,BirthDate = updatedEmployee.BirthDate,
                HireDate = updatedEmployee.HireDate,City = updatedEmployee.City, ReportsTo = updatedEmployee.ReportsTo ,Title = updatedEmployee.Title
            };

            _repositoryEmployee.UpdateEmployee(employee);
        }
        //[HttpPut("UpdateEmployee")]
        //public IActionResult UpdateEmployee(int id, [FromBody] EmployeeViewModel employee)
        //{
        //    try
        //    {
        //        Employee tempemp = _repositoryEmployee.MapEmpViewModelToEmployee(employee);
        //        tempemp.EmployeeId = id;
        //        int result = _repositoryEmployee.UpdateEmployee(tempemp);
        //        if (result == 0)
        //        {
        //            return NotFound("Employee not found");
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal Server Error: " + ex.Message);
        //    }
        //}
        [HttpDelete("/DeleteEmployee")]
        public int DeleteEmployee(int id)
        {
            _repositoryEmployee.DeleteEmployee(id);
            return 1;
        }
    }
}

