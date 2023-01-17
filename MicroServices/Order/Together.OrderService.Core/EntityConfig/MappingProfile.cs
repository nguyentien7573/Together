﻿using AutoMapper;
using Together.AppContracts.Dtos.Order;
using Together.OrderService.Core.Entities;

namespace Together.OrderService.Core.EntityConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
