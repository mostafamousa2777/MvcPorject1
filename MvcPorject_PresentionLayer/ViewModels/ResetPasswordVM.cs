using System.ComponentModel.DataAnnotations;

namespace MvcPorject_PresentionLayer.ViewModels
{
	public class ResetPasswordVM
	{
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirmed Password is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "does not match password")]
		public string ConfirmPassword { get; set; }
	}
}
