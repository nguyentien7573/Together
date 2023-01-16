using AutoMapper;
using FluentValidation;
using MassTransit;
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
                private readonly IPublishEndpoint _publishEndpoint;

                public Handler(IRepository<Order> orderRepository, IPublishEndpoint publishEndpoint)
                {
                    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                    _publishEndpoint = publishEndpoint;
                }

                public async Task<ResultModel<OrderDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var message = request.Model;
                    await _publishEndpoint.Publish<OrderDto>(message);
                    return null;
                }
            }
        }
    }
}
