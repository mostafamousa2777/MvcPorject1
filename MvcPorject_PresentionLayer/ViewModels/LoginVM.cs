using System.ComponentModel.DataAnnotations;

namespace MvcPorject_PresentionLayer.ViewModels
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Email is Required"), EmailAddress(ErrorMessage = "Invalid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}
