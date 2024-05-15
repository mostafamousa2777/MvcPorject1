using DataAccess_Layer.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcPorject_PresentionLayer.Utility;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _SignInManager;
		public AccountController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_SignInManager = signInManager;
		}

		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task< IActionResult> Register(RegisterVM registerVM)
		{
			if(!ModelState.IsValid)
                return View(registerVM);
            var user = new AppUser
            {
                LName = registerVM.LName,
                Fname = registerVM.Fname,
                Email = registerVM.Email,
                Agree = registerVM.Agree,
                UserName = registerVM.Fname + registerVM.LName 
			};
          var result=  await _userManager.CreateAsync(user,registerVM.Password);

            if (result.Succeeded) return RedirectToAction(nameof(Login));

            foreach (var error in result.Errors) {

                ModelState.AddModelError("", error.Description);
            }

			return View(registerVM);

		}
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
		public async Task< IActionResult> Login(LoginVM loginVM)
		{
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null) {
                if (await _userManager.CheckPasswordAsync(user, loginVM.Password)) {
                 var result=   await _SignInManager.
                              PasswordSignInAsync(user, loginVM.Password, loginVM.Rememberme, false);

                    if(result.Succeeded)return RedirectToAction("Index","Home");
                }
            
            }
            ModelState.AddModelError("", "Incorrect email or Password");



			return View();
		}
        public new async Task< IActionResult> SignOut() {

            await _SignInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
		}
		public IActionResult ForgetPassword()
		{
			return View();
		}
        [HttpPost]
		public async Task<IActionResult> ForgetPasswordAsync(Forget_PasswordVM modelforget)
		{
            if (!ModelState.IsValid) return View(modelforget);

			var user = await _userManager.FindByEmailAsync(modelforget.Email);
			if (user != null)
            {
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);


				var url = Url.Action
                    
                ("ResetPassword", "Account", new { email=modelforget.Email, usertoken= token },Request.Scheme);

                var email = new Email
                {
                    Recipient = modelforget.Email,
                    Subject = "reset Email",
                    Body = url

				};
                MailSetting.SendEmail(email);
				return RedirectToAction(nameof(CheckYourInBox));

			}
			ModelState.AddModelError("", "Incorrect email ");

			return View();
		}
		public IActionResult CheckYourInBox()
		{
			return View();
		}
		public IActionResult ResetPassword(string Email,String Token)
		{
			TempData["email"]=Email;
			TempData["token"]=Token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
		{
			if (!ModelState.IsValid) return View();

			var email = TempData["email"] as string;
			var token = TempData["token"] as string;
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = _userManager.ResetPasswordAsync(user, token, model.Password);
				if (result.IsCompletedSuccessfully) return RedirectToAction(nameof(Login));
				
			}
			return View();

		}

	}
}
