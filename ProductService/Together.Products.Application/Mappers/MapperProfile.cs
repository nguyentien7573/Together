using AutoMapper;
using Together.Products.Application.Responses;
using Together.Products.Core.Entities;

namespace Together.Products.Application.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
