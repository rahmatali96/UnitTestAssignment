using UnitTestAssignment.Models;

namespace UnitTestAssignment.Services
{
    public interface IEmployeeService
    {
        void Add<K>(K entity) where K : class;
        Task<bool> SaveChangesAsync();

        // Camps
        Task<Employee[]> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<Employee> GetEmployeeByDeptIdAsync(int deptid);
        Task<Department> GetDepartmentByDeptIdAsync(int deptid);
        Task<Employee> PostEmployee(Employee employee);


    }
}
