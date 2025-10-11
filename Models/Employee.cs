using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCURD.Models          // <-- your project name
{
    [Table("Employee")] // DB table name
    public class Employee
    {
        [Key, Required]
        public int EmployeeNumber { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public string EmployeeName { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required, StringLength(50)]
        public string Job { get; set; } = default!;

        [Required, StringLength(50)]
        public string DepartmentName { get; set; } = default!;
    }
}
