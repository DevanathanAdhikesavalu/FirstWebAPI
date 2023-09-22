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
        [HttpPut]
        public Employee EditEmployee(int id , [FromBody] Employee updatedEmployee)
        {
            
            updatedEmployee.EmployeeId = id; // Ensure the ID in the URL matches the EmployeeId

            Employee savedEmployee = _repositoryEmployee.UpdateEmployee(updatedEmployee);
            return savedEmployee;
        }
        [HttpDelete]
        public int DeleteEmployee(int id)
        {
            _repositoryEmployee.DeleteEmployee(id);
            return 1;
        }
    }
}

