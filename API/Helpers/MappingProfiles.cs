using API.Dto;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, EditUserDto>();
            CreateMap<AppRole, RoleDto>();
        }
        
    }
}