using AutoMapper;
using DataAccess_Layer.models;
using MvcPorject_PresentionLayer.ViewModels;

namespace MvcPorject_PresentionLayer.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserVM>();
        }
    }
}
