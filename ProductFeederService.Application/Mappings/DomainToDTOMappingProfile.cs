using AutoMapper;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Domain.Entities;
using ProductFeederService.ExternalAPI.Request;
using ProductFeederService.ExternalAPI.Response;

namespace ProductFeederService.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            
            CreateMap<CategoryAPI, CategoryAPIDTO>().ReverseMap();
            CreateMap<ProductAPI, ProductAPIDTO>().ReverseMap();  
            CreateMap<CategoryAPI, ResponseCategory>().ReverseMap();    
            CreateMap<ProductAPI, ResponseProduct>().ReverseMap();      
            // CreateMap<CategoryAPIDTO, CategoryAPI>().ReverseMap();
            // CreateMap<ProductAPIDTO, ProductAPI>().ReverseMap();  
            CreateMap<CategoryAPI, RequestCategory>().ReverseMap();    
            CreateMap<ProductAPI, RequestProduct>().ReverseMap(); 
        }
    }
}