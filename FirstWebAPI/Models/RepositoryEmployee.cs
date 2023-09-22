using Microsoft.EntityFrameworkCore;

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
        public Employee UpdateEmployee(Employee updatedEmployee) 
        {
            _context.Employees.Update(updatedEmployee);
           // Console.WriteLine(_context.Entry(updatedEmployee).State); 
            _context.SaveChanges();
            return updatedEmployee;
        }
        public void AddNewEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        public int DeleteEmployee(int id)
        {

            var employee = _context.Employees.Find(id);

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return 1;

        }
    }
}
