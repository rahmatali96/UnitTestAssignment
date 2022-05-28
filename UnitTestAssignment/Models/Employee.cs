using System.ComponentModel.DataAnnotations;

namespace UnitTestAssignment.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; } 
        public string Address { get; set; }
        public int DeptId { get; set; }

    }
}
