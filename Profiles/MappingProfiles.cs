using AutoMapper;
using ProductApi.Entities;
using ProductApi.Entities.Auth;
using ProductApi.Entities.Dtos.Auth;
using ProductApi.Entities.Dtos.Categories;
using ProductApi.Entities.Dtos.Products;

namespace ProductApi.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();


            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, GetProductDto>();
            CreateMap<UpdateProductDto, Product>();


            CreateMap<RegisterDto, AppUser>().ReverseMap();





        }
    }
}
