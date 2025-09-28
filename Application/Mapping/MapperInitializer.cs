using Application.Dtos.Request.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
                
            CreateMap<Product, ProductDto>()
                .ForMember(x=>x.CategoryName, op => op.MapFrom( src=>src.Category.Name)).ReverseMap();
            CreateMap<CreateProductDto, Product>()
                .ForMember(m => m.Photos, op=>op.Ignore()).ReverseMap();
            CreateMap<UpdateCategoryDto, Product>()
                .ForMember(m => m.Photos, op => op.Ignore()).ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();



            //CreateMap<Photo, CreatePhotoDto>().ReverseMap();
        }
    }



}
