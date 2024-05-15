using System.ComponentModel.DataAnnotations;

namespace MvcPorject_PresentionLayer.ViewModels
{
	public class RegisterVM
	{
		public string Fname { get; set; }
		public string LName { get; set; }
		[Required(ErrorMessage ="Email is Required"),EmailAddress(ErrorMessage = "Invalid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirmed Password is Required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="does not match password")]
		public string ConfirmPassword { get; set; }
		public bool Agree { get; set; }





	}
}
