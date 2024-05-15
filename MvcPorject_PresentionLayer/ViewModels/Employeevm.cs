using DataAccess_Layer.models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcPorject_PresentionLayer.ViewModels
{
    public class Employeevm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Salary { get; set; }
        [Range(22, 60)]
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }

    }
}
