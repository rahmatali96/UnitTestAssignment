using Microsoft.EntityFrameworkCore;
using UnitTestAssignment.Models;

namespace UnitTestAssignment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmpDeptDbContext _context;

        public EmployeeService(EmpDeptDbContext context)
        {
            _context = context;
        }

        public void Add<K>(K entity) where K : class
        {
            _context.Add(entity);
        }

        public Task<Employee[]> GetAllEmployeeAsync()
        {
            return _context.Employees.ToArrayAsync(); 
        }

        public Task<Department> GetDepartmentByDeptIdAsync(int deptid)
        {
            var data = _context.Departments.Where(o=>o.DeptId == deptid).FirstOrDefaultAsync();
            return data;
        }

        public Task<Employee> GetEmployeeByDeptIdAsync(int deptid)
        {
            var data = _context.Employees.Where(o => o.DeptId == deptid).FirstOrDefaultAsync();
            return data;
        }
        public Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var data = _context.Employees.Where(o => o.Email == email).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Employee> PostEmployee(Employee employee)
        {
            var data =_context.Add(employee);
            _context.SaveChangesAsync();
            return data.Entity;

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
