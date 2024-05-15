using System.ComponentModel.DataAnnotations;

namespace MvcPorject_PresentionLayer.ViewModels
{
	public class Forget_PasswordVM
	{
		[Required(ErrorMessage = "Email is Required"), EmailAddress(ErrorMessage = "Invalid")]
		public string Email { get; set; }
	}
}
