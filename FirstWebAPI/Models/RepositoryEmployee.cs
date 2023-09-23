using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace FirstWebAPI.Models
{
    public class RepositoryEmployee
    {
        private NorthwindContext _context;
        public RepositoryEmployee(NorthwindContext context)
        {
            _context = context;
        }
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee FindEmployeeById(int id) 
        {
            Employee employee=_context.Employees.Find(id);
            return employee;
        }
        public int UpdateEmployee(Employee updatedEmployee) 
        {
            EntityState es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState b4 Update :{es.GetDisplayName()}");

            _context.Employees.Update(updatedEmployee);

            es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState After Update :{es.GetDisplayName()}");

            _context.SaveChanges();

            es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState After SaveChanges :{es.GetDisplayName()}");

            return 1;
        }
        public void AddNewEmployee(Employee employee)
        {
            Employee? foundEmp = _context.Employees.Find(employee.EmployeeId);
            if(foundEmp != null)
            {
                throw new Exception("Failed to add new employee.Duplicate Id");
            }
            EntityState es = _context.Entry(employee).State;
            Console.WriteLine($"EntityState b4 Add :{es.GetDisplayName()}");

            _context.Employees.Add(employee); 

            es = _context.Entry(employee).State;
            Console.WriteLine($"EntityState b4 Add :{es.GetDisplayName()}");

            _context.SaveChanges();   

            es = _context.Entry(employee).State;
            Console.WriteLine($"EntityState b4 Add :{es.GetDisplayName()}");
        }
        public int DeleteEmployee(int id)
        {

            Employee? employee = _context.Employees.Find(id);
            EntityState es = EntityState.Detached;
            int result = 0;
            if (employee != null)
            {
                es = _context.Entry(employee).State;
                Console.WriteLine($"EntityState Before Delete :{es.GetDisplayName()}");

                _context.Employees.Remove(employee);

                es = _context.Entry(employee).State;
                Console.WriteLine($"EntityState after Delete :{es.GetDisplayName()}");

                result = _context.SaveChanges();

                es = _context.Entry(employee).State;
                Console.WriteLine($"EntityState after SaveChanges :{es.GetDisplayName()}");
            }
            return result;

        }
        public EmployeeViewModel MapEmployeeToEmpViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                EmpId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                City = employee.City,
                ReportsTo = employee.ReportsTo > 0 ? employee.ReportsTo : null
            };
        }
        public Employee MapEmpViewModelToEmployee(EmployeeViewModel empViewModel)
        {
            return new Employee
            {
                FirstName = empViewModel.FirstName,
                LastName = empViewModel.LastName,
                Title = empViewModel.Title,
                BirthDate = empViewModel.BirthDate,
                HireDate = empViewModel.HireDate,
                City = empViewModel.City,
                ReportsTo = empViewModel.ReportsTo > 0 ? empViewModel.ReportsTo : null
            };
        }
    }
}
