using AutoMapper;
using Together.AppContracts.Dtos.Product;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.EntityConfig
{
    public class ProductConfig
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductDto>();
        }
    }
}
