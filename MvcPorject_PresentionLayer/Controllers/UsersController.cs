using AutoMapper;
using DataAccess_Layer.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;


        public UsersController(UserManager<AppUser> userManager, IMapper mapper )
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async  Task< IActionResult> Index(string Email)
		{
			if(string.IsNullOrWhiteSpace(Email))
			{
				var users=await _userManager.Users.Select(x => new UserVM {
				Id = x.Id,
				Fname=x.Fname,
				LName=x.LName,
				Email=x.Email,
				Roles=_userManager.GetRolesAsync(x).Result
				
				}).ToListAsync();
				return View(users);
			}
			var user =await _userManager.FindByEmailAsync(Email);
			if (user == null) return View(Enumerable.Empty<UserVM>());
			var mapuser = new UserVM
			{
				Email = user.Email,
				Fname = user.Fname,
				LName = user.LName,
				Id = user.Id,
				Roles= _userManager.GetRolesAsync(user).Result



			};
			return View(new List<UserVM> { mapuser });
		}
        public async Task< IActionResult> Details(string id,string viewname= "Details") {
			if (string.IsNullOrWhiteSpace(id)) return BadRequest();
			var user=await _userManager.FindByIdAsync(id);
			if (user == null) return NotFound();
			var mapper=_mapper.Map<AppUser,UserVM>(user);
			mapper.Roles =await _userManager.GetRolesAsync(user);
			return View(viewname,mapper);

        }
		public async Task<IActionResult> Edit(string id) {

			return await Details(id, nameof(Edit));

        }
		[HttpPost]
        public async Task<IActionResult> Edit(string id,UserVM model)
        {

          if(id!=model.Id) return BadRequest();
			if (!ModelState.IsValid) return View(model);
			try {
				var user = await _userManager.FindByIdAsync(model.Id); 
				
				if (user == null) return NotFound();
				user.Fname = model.Fname;
				user.LName=model.LName;
				await _userManager.UpdateAsync(user);
				return RedirectToAction(nameof(Index));
			
			}

			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty,ex.Message);
			}
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {

            return await Details(id, nameof(Delete));

        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null) return NotFound();
                

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }





    }
}
