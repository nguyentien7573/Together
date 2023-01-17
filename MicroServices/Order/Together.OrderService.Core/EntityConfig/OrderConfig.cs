using AutoMapper;
using Together.AppContracts.Dtos.Order;
using Together.OrderService.Core.Entities;

namespace Together.OrderService.Core.EntityConfig
{
    public class OrderConfig
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, OrderDto>();
            cfg.CreateMap<OrderDto, Order>();
            cfg.CreateMap<OrderItem, OrderItemDto>();
            cfg.CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
