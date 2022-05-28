using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using UnitTestAssignment.Controllers;
using UnitTestAssignment.Models;
using UnitTestAssignment.Services;

namespace AssignmentTestProject
{
    [TestFixture]
    public class EmployeeServiceTest
    {
        private EmpDeptDbContext _context;
        private EmpDeptDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<EmpDeptDbContext>();
            options.UseSqlServer("DatabaseName:UnitTestDb ");
            var context = new EmpDeptDbContext(options.Options);
            context.Departments.Add(new Department { DeptId = 1, DeptName = "HR" });
            context.Departments.Add(new Department { DeptId = 2, DeptName = "Development" });
            context.Departments.Add(new Department { DeptId = 2, DeptName = "Management" });
            context.Employees.Add(new Employee { EmpId = 1,Name = "Melody",Email = "melody@gmail.com", Mobile = 7894561230, Address = "Delhi", DeptId = 1});
            context.Employees.Add(new Employee { EmpId = 1, Name = "Jackey", Email = "jackey@gmail.com", Mobile = 4578542112, Address = "Lucknow", DeptId = 2 });
            context.Employees.Add(new Employee { EmpId = 1, Name = "Molley", Email = "molley@gmail.com", Mobile = 2154875454, Address = "Mumbai", DeptId = 1 });
            context.SaveChangesAsync();
            return context;

        }

        [SetUp]
        public void Setup()
        {
            _context = GetContext();
            _context.Database.EnsureCreated();
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
        [Test]
        public async Task Get_whenCalled_ShouldReturnTwoRecords()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var data = await employeeServie.GetAllEmployeeAsync();
            Assert.IsInstanceOf<OkObjectResult>(data);
        }
        [Test]
        public async Task GetEmployeeById_whenValidIdPassed_ShouldReturnRecord()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var data = await employeeServie.GetEmployeeByEmailAsync("Amy@gmail.com");
            Assert.IsInstanceOf<OkObjectResult>(data);
        }

        [Test]
        public async Task GetEmployeeById_whenInvalidIdPassed_ShouldReturnBadeRequest()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var data = await employeeServie.GetEmployeeByEmailAsync("Rony@gmail.com");
            Assert.IsInstanceOf<NotFoundResult>(data);
        }
        [Test]
        public async Task Post_whenNewEmployeeWithValidDepartmentSend_ShouldAddEmployee()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var employee = new Employee
            {
                EmpId = 4,
                Name = "Rockey",
                Email = "rockey@gmail.com",
                Mobile = 4587215487,
                Address="Pune",
                DeptId=3
            };
            var data = await employeeServie.PostEmployee(employee);
            Assert.IsInstanceOf<CreatedResult>(data);
        }

        [Test]
        public async Task Post_whenExsistingEmployeeWithValidDepartmentSend_ShouldThrowError()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var employee = new Employee
            {
                EmpId = 4,
                Name = "Rockey",
                Email = "rockey@gmail.com",
                Mobile = 4587215487,
                Address = "Pune",
                DeptId = 3
            };
            var data = await employeeServie.PostEmployee(employee);
            Assert.IsInstanceOf<BadRequestResult>(data);
        }
        [Test]
        public async Task Post_whenNewEmployeeWithInvalidDepartmentSend_ShouldThrowError()
        {
            EmployeeService employeeServie = new EmployeeService(_context);
            var employee = new Employee
            {
                EmpId = 4,
                Name = "Rockey",
                Email = "rockey@gmail.com",
                Mobile = 4587215487,
                Address = "Pune",
                DeptId = 3
            };
            var data = await employeeServie.PostEmployee(employee);
            Assert.IsInstanceOf<BadRequestResult>(data);
        }
        [Test]

        public void GetEmployeeCountByDepartment_whenCalled_ShouldReturn2Value()
        {
            EmployeeService employeeService = new EmployeeService(_context);
            var data = employeeService.GetEmployeeByDeptIdAsync(1);
            Assert.That(data, Is.EqualTo(2));

        }
    }
}