using AutoMapper;
using KfcApi.DTOs;
using KfcApi.Models;
using Microsoft.Extensions.Logging;

namespace KfcApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
               .ForMember(dest => dest.HomeCategory, opt => opt.MapFrom(src => src.HomeCategory.Name))
               .ReverseMap();

            CreateMap<Product, ProductRequestDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationRequestDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryRequestDto>().ReverseMap();
            CreateMap<HomeCategory, HomeCategoryDto>().ReverseMap();
            CreateMap<HomeCategory, HomeCategoryRequestDto>().ReverseMap();
            CreateMap<AddOn, AddOnDto>().ReverseMap();
            CreateMap<AddOn, AddOnRequestDto>().ReverseMap();
            CreateMap<MenuAddOn, MenuAddOnDto>().ReverseMap();
            CreateMap<MenuAddOn, MenuAddOnRequestDto>().ReverseMap();
            CreateMap<Cart, CartRequestDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Cart, UpdateCartRequestDto>().ReverseMap();
            CreateMap<OrderUser, OrderUserDto>().ReverseMap();
            CreateMap<OrderUser, OrderUserRequestDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderRequestDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressRequestDto>().ReverseMap();
            CreateMap<Address, AddressUpdateRequestDto>().ReverseMap();

            //CreateMap<UpdateUserRequestDto, ApplicationUser>().ForMember(dest => dest.Image, opt => opt.Ignore()).ReverseMap();

        }
    }
}
