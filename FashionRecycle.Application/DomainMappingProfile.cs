using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;

namespace FashionRecycle.Application
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<CreateUserInputModel, UserEntity>()
                .ForMember(x => x.UserName, x => x.MapFrom(d => d.UserName))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.Password, x => x.MapFrom(d => d.Password))
                .ReverseMap();

            CreateMap<UserViewModel, UserEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))              
                .ReverseMap();
        }
    }
}
