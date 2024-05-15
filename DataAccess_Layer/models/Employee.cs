using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess_Layer.models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName ="Money")]
        public Decimal Salary { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ImageName { get; set; }

        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime HireDate { get; set; }



    }
}
