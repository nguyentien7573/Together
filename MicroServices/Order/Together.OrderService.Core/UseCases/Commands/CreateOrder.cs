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

            public record CreateOrdertModel(Guid customerId, string address, string address1, string address2, string districtCode, string provincesCode, string wardCode, float total, List<OrderItem> OrderItems);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.customerId)
                        .NotEmpty().WithMessage("CustomerId is required.");

                    RuleFor(x => x.Model.total)
                        .GreaterThanOrEqualTo(0).WithMessage("Quantity should at least greater than or equal to 0.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<OrderDto>>
            {
                private readonly IPublishEndpoint _publishEndpoint;

                public Handler(IPublishEndpoint publishEndpoint)
                {
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
