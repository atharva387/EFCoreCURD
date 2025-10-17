using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCURD.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, StringLength(100)]
        public string DepartmentName { get; set; } = default!;

        [StringLength(100)]
        public string? Location { get; set; }
    }
}
