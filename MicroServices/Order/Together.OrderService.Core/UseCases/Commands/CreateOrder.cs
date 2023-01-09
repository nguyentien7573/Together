using AutoMapper;
using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Order;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.OrderService.Core.Entities;
using Together.OrderService.Core.EntityConfig;

namespace Together.OrderService.Core.UseCases.Commands
{
    public class CreateOrder
    {
        public record Command : ICreateCommand<Command.CreateOrdertModel, OrderDto>
        {
            public CreateOrdertModel Model { get; init; } = default!;

            public record CreateOrdertModel(Guid CustomerId, string Address, string Address1, string Address2, string DistrictCode, string ProvincesCode, string WardCode, float TotalCost, string Description, string Status, List<OrderItem> OrderItems);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.CustomerId)
                        .NotEmpty().WithMessage("CustomerId is required.");

                    RuleFor(x => x.Model.TotalCost)
                        .GreaterThanOrEqualTo(0).WithMessage("Quantity should at least greater than or equal to 0.");

                    RuleFor(x => x.Model.Status)
                        .NotEmpty().WithMessage("Category is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<OrderDto>>
            {
                private readonly IRepository<Order> _orderRepository;

                public Handler(IRepository<Order> orderRepository)
                {
                    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                }

                public async Task<ResultModel<OrderDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var created = await _orderRepository.AddAsync(
                        Order.Create(
                            request.Model.CustomerId,
                            request.Model.TotalCost,
                            request.Model.Address,
                            request.Model.Address1,
                            request.Model.Address2,
                            request.Model.ProvincesCode,
                            request.Model.WardCode,
                            request.Model.DistrictCode,
                            request.Model.Status,
                            request.Model.Description,
                            request.Model.OrderItems));

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new MappingProfile());
                    });
                    var mapper = config.CreateMapper();
                    var orderItems = new List<OrderItemDto>();

                    if (created.OrderItems.Any())
                    {
                        foreach (var orderItem in created.OrderItems)
                        {
                            orderItems.Add(mapper.Map<OrderItem, OrderItemDto>(orderItem));
                        }
                    }

                    return ResultModel<OrderDto>.Create(new OrderDto
                    {
                        Id = created.Id,
                        CustomerId = created.CustomerId,
                        TotalCost = created.TotalCost,
                        Address = created.Address,
                        Status = created.Status,
                        Description = created.Description,
                        OrderItems = orderItems,
                    });
                }
            }
        }
    }
}
