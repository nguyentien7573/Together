using AutoMapper;
using Together.Products.Application.Responses;
using Together.Products.Core.Entities;

namespace Together.Products.Application.Mappers
{
    public class MapperConfig
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponse>();
                cfg.AddProfile<MapperProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
