using System.ComponentModel.DataAnnotations;

namespace DataAccess_Layer.models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        [Range(10,100)]
        public int Code { get; set; }
        public ICollection<Employee> employees { get; set; } = new List<Employee>();
    }
}
