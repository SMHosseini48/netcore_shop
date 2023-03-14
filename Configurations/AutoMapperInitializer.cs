using AutoMapper;
using ncorep.Dtos;
using ncorep.Models;

namespace ncorep.Configurations;

public class AutoMapperInitializer : Profile
{
    public AutoMapperInitializer()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

        CreateMap<AppUser, UserRegisterDto>().ReverseMap();
        CreateMap<AppUser, UserLoginDTO>().ReverseMap();
        CreateMap<AppUser, UserLoginResponseDto>().ReverseMap();
        CreateMap<AppUser, CustomerUpdateDto>().ReverseMap();

        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDTO>().ReverseMap();

        CreateMap<Image, ImageDto>().ReverseMap();
        CreateMap<Image, ImageCreateDto>().ReverseMap();

       
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Order, OrderCreateDTO>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();


        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressCreateDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();

        CreateMap<ShoppingCartRecord, ShoppingCartRecordDTO>().ReverseMap();
        CreateMap<ShoppingCartRecord, ShoppingCartRecordUpdateDto>().ReverseMap();
        CreateMap<ShoppingCartRecord, ShoppingCartRecordCreateDto>().ReverseMap();
    }
}