using AutoMapper;
using DataAccess_Layer.models;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.MappingProfile
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employeevm, Employee>().ReverseMap();//ForMember(d=>d.Name,opt=>opt.MapFrom(s=>s.EmployeeName));
        }
    }
}
