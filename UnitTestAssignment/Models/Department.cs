using System.ComponentModel.DataAnnotations;

namespace UnitTestAssignment.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }
}
