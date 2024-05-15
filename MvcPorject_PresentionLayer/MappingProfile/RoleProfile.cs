using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.MappingProfile
{
    public class RoleProfile:Profile
        
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleVM>().ReverseMap();
        }
    }
}
