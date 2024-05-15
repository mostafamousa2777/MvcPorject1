using AutoMapper;
using Business_Layer.Interfaces;
using DataAccess_Layer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcPorject_PresentionLayer.Utility;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.Controllers

{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;

        //private readonly IEmployeeRepoistory _repo;
        //private readonly IDepartmentRepo _departmentRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IUniteOfWork uniteOfWork,IMapper mapper)
        {
           
           _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
        public async  Task< IActionResult> Index(string? SearchValue)
        {
            if (string.IsNullOrWhiteSpace(SearchValue)) {
                var employees = await _uniteOfWork.Employees.GetAllAsync();
                return View(_mapper.Map<IEnumerable<Employeevm>>(employees));
            }
          var  employeess=await _uniteOfWork.Employees.GetAllByNameAsync(SearchValue);
            return View(_mapper.Map<IEnumerable<Employeevm>>(employeess));
        }
        public async Task< IActionResult> Create()
        {
            ViewBag.departments= await _uniteOfWork.Departments.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(Employeevm employeevm)
        {
            if (ModelState.IsValid)
            {
                if (employeevm.Image is not null)
                {
                    employeevm.ImageName = DocumentSetting.UploadFile(employeevm.Image, "Images");

                }
                var  employee=_mapper.Map<Employeevm,Employee>(employeevm);
               await _uniteOfWork.Employees.AddAsync(employee);
              await  _uniteOfWork.CompeleteAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.departments = await _uniteOfWork.Departments.GetAllAsync();

            return View(employeevm);
        }
        public async Task<IActionResult> Details(int? id) => await ReturnViewWithEmployee(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) => await ReturnViewWithEmployee(id, nameof(Edit));

        [HttpPost]
        public async Task< IActionResult> Edit(Employeevm employeevm, [FromRoute] int id)
        {
            if (id != employeevm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeevm.Image is not null)
                    {
                        employeevm.ImageName = DocumentSetting.UploadFile(employeevm.Image, "Images");

                    }
                    _uniteOfWork.Employees.Update(_mapper.Map<Employeevm,Employee>(employeevm));
                   await _uniteOfWork.CompeleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(employeevm);


        }
        public async Task<IActionResult> Delete(int? id) =>await ReturnViewWithEmployee(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete(Employeevm employeevm, [FromRoute] int id)
        {
            if (id != employeevm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _uniteOfWork.Employees.Delete(_mapper.Map<Employeevm, Employee>(employeevm));
                    if (await _uniteOfWork.CompeleteAsync() > 0 && employeevm.ImageName is not null)
                        DocumentSetting.DeleteFile("Images", employeevm.ImageName);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(employeevm);


        }
        private async  Task< IActionResult> ReturnViewWithEmployee(int? id, string ViewName)
        {
            if (id.Equals(null))
            {
                return BadRequest();
            }
            var employee = await _uniteOfWork.Employees.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.departments = await _uniteOfWork.Departments.GetAllAsync();


            return View($"{ViewName}", _mapper.Map<Employeevm>(employee));



        }
    }
}
