using AutoMapper;
using Together.AppContracts.Dtos.Product;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.EntityConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
