using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Identity.User;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(destination => destination.ProductBrand, options => options.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.ProductType,  options => options.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<AppUser, UserDto>()
                .ForMember(destination => destination.Token, options => options.MapFrom<UserTokenResolver>()); 

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}