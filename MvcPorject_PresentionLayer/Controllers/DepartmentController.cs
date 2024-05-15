using Business_Layer.Interfaces;
using DataAccess_Layer.models;
using Microsoft.AspNetCore.Mvc;

namespace MvcPorject_PresentionLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _repo;

        public DepartmentController( IDepartmentRepo repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Mes"] = "hello";
            var department=await _repo.GetAllAsync();
            return View(department);
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create( Department department)
        {
            if (ModelState.IsValid)
            {
               await _repo.AddAsync(department);
                TempData["mes"] = "added succefully";
                return RedirectToAction(nameof(Index));
            }
           return View(department);
        }
        public async Task<IActionResult> Details( int? id)=>await ReturnViewWithDepartment(id,nameof(Details));
        
        public async Task<IActionResult> Edit(int? id) => await ReturnViewWithDepartment(id, nameof(Edit));

        [HttpPost]
        public IActionResult Edit(Department department,[FromRoute]int id)
        {
            if(id!=department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
               
            }
            return View(department);


        }
        public async Task<IActionResult> Delete(int? id) => await ReturnViewWithDepartment(id, nameof(Delete));

        [HttpPost]
        public IActionResult Delete(Department department,[FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(department);


        }
        private async Task< IActionResult> ReturnViewWithDepartment(int? id, string ViewName) {
            if (id.Equals(null))
            {
                return BadRequest();
            }
            var department = await _repo.GetByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View($"{ViewName}",department);



        }


    }
}
