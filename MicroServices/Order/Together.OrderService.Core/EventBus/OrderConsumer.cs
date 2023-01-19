using AutoMapper;
using MassTransit;
using Together.AppContracts.Dtos.Order;
using Together.Core.Repository;
using Together.OrderService.Core.Entities;
using Together.OrderService.Core.EntityConfig;

namespace Together.OrderService.Core.EventBus
{
    public class OrderConsumer : IConsumer<OrderDto>
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderConsumer(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<OrderDto> context)
        {
            var request = context.Message;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            var orderItems = new List<OrderItem>();

            if (request.OrderItems.Any())
            {
                foreach (var orderItem in request.OrderItems)
                {
                    orderItems.Add(mapper.Map<OrderItemDto, OrderItem>(orderItem));
                }
            }

            var created = await _orderRepository.AddAsync(
                      Order.Create(
                          request.CustomerId,
                          request.Total,
                          request.Address,
                          request.Address1,
                          request.Address2,
                          request.ProvincesCode,
                          request.WardCode,
                          request.DistrictCode,
                          orderItems));
        }
    }
}
