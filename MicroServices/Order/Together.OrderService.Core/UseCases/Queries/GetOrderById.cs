using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Category;
using Together.AppContracts.Dtos.Order;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.OrderService.Core.Entities;

namespace Together.OrderService.Core.UseCases.Queries
{
    public class GetOrderById
    {
        public record Query : IItemQuery<Guid, OrderDto>
        {
            public List<string> Includes { get; init; } = new();
            public Guid Id { get; init; }

            internal class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.Id)
                        .NotNull()
                        .NotEmpty().WithMessage("Id is required.");
                }
            }

            internal class Handler : IRequestHandler<Query, ResultModel<OrderDto>>
            {
                private readonly IRepository<Order> _orderRepository;

                public Handler(IRepository<Order> orderRepository)
                {
                    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                }

                public async Task<ResultModel<OrderDto>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var order = await _orderRepository.FindById(request.Id);

                    if (order == null)
                    {
                        return null;
                    }

                    return ResultModel<OrderDto>.Create(new OrderDto
                    {
                        Id= order.Id,
                    });
                }
            }
        }
    }
}
