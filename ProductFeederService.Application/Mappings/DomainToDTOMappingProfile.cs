using AutoMapper;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Domain.Entities;

namespace ProductFeederService.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();            
        }
    }
}