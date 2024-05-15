using AutoMapper;
using DataAccess_Layer.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper _mapper;


        public RolesController(RoleManager<IdentityRole> rolemanager, IMapper mapper)
        {
            _rolemanager = rolemanager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var roles = await _rolemanager.Roles.Select(r => new RoleVM
                {
                    Id = r.Id,

                    Name = r.Name

                }).ToListAsync();
                return View(roles);
            }
            var role = await _rolemanager.FindByNameAsync(name);
            if (role == null) return View(Enumerable.Empty<RoleVM>());
            var maprole = new RoleVM
            {
                
                Id = role.Id,
                Name = role.Name



            };
            return View(new List<RoleVM> { maprole });

        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(RoleVM model)
        {
            if (ModelState.IsValid) {

                var mapperrole = _mapper.Map<IdentityRole>(model);
                var result=await _rolemanager.CreateAsync(mapperrole);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors) {

                    ModelState.AddModelError("", error.Description);
                }
               
            
            }
            return View(model);
        }
        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var role = await _rolemanager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var mapperole = _mapper.Map<IdentityRole, RoleVM>(role);
            return View(viewname, mapperole);

        }
        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, nameof(Edit));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleVM model)
        {

            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            try
            {
                var role = await _rolemanager.FindByIdAsync(model.Id);

                if (role == null) return NotFound();
                role.Name = model.Name;
               
                await _rolemanager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
                var role = await _rolemanager.FindByIdAsync(id);

                if (role == null) return NotFound();


                await _rolemanager.DeleteAsync(role);
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
