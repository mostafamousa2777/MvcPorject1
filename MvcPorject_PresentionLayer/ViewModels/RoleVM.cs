using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcPorject_PresentionLayer.ViewModels
{
    public class RoleVM
    {
       
        public string  Id { get; set; }
        public string Name { get; set; }
        public RoleVM()
        {
            Id= Guid.NewGuid().ToString();
        }
    }
}
