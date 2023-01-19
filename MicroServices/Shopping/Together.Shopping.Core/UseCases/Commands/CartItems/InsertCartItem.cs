using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Shopping.CartItem;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Core.UseCases.Commands.CartItems
{
    public class InsertCartItem
    {
        public record Command : ICreateCommand<Command.CartItemModel, CartItemDto>
        {
            public CartItemModel Model { get; init; } = default!;

            public record CartItemModel(Guid SessionId, Guid ProductId, int Quantity);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.ProductId)
                        .NotEmpty().WithMessage("ProductId is required.");
                    RuleFor(v => v.Model.SessionId)
                       .NotEmpty().WithMessage("SessionId is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<CartItemDto>>
            {
                private readonly IRepository<CartItem> _cartItemRepository;

                public Handler(IRepository<CartItem> cartItemRepository)
                {
                    _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
                }

                public async Task<ResultModel<CartItemDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var created = await _cartItemRepository.AddAsync(
                        CartItem.Create(
                            request.Model.SessionId,
                            request.Model.ProductId,
                            request.Model.Quantity));

                    return ResultModel<CartItemDto>.Create(new CartItemDto
                    {
                        Id = created.Id,
                        SessionId = created.SessionId,
                        ProductId = request.Model.ProductId,
                        Quantity = request.Model.Quantity,
                        CreatedOn = created.CreatedOn,
                        UpdatedOn = created.UpdatedOn,
                    });
                }
            }
        }
    }
}
