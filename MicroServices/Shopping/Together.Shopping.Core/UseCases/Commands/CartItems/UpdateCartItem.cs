using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Shopping.CartItem;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Core.UseCases.Commands.CartItems
{
    public class UpdateCartItem
    {
        public record Command : IRequest<bool>
        {
            public UpdateCartItemModel Model { get; init; } = default!;

            public record UpdateCartItemModel(Guid Id, Guid SessionId, Guid ProductId, int Quantity, bool IsActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.SessionId)
                        .NotEmpty().WithMessage("SessionId is required.");

                    RuleFor(x => x.Model.ProductId)
                        .NotEmpty().WithMessage("ProductId is required.");

                    RuleFor(x => x.Model.Quantity)
                        .NotEmpty().WithMessage("Quantity is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, bool>
            {
                private readonly IRepository<CartItem> _cartItemRepository;

                public Handler(IRepository<CartItem> cartItemRepository)
                {
                    _cartItemRepository = cartItemRepository;
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                    {
                        return false;
                    }

                    var prod = await _cartItemRepository.FindById(request.Model.Id);
                    if (prod == null)
                    {
                        return false;
                    }

                    prod.SessionId = request.Model.SessionId;
                    prod.ProductId = request.Model.ProductId;
                    prod.Quantity = request.Model.Quantity;
                    prod.IsActive = request.Model.IsActive;

                    var result = await _cartItemRepository.Update(prod);

                    return result;
                }
            }
        }
    }
}
