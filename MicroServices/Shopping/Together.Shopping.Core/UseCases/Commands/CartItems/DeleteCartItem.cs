using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Core.Repository;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Core.UseCases.Commands.CartItems
{
    public class DeleteCartItem
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; } = default!;
            public record DeleteProductModel(Guid Id);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Id)
                        .NotEmpty().WithMessage("Id is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, bool>
            {
                private readonly IRepository<CartItem> _cartItemRepository;

                public Handler(IRepository<CartItem> productRepository)
                {
                    _cartItemRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }
                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                        return false;

                    var prod = await _cartItemRepository.FindById(request.Id);

                    if (prod == null)
                        return false;

                    prod.IsActive = false;

                    return await _cartItemRepository.Update(prod);
                }
            }
        }
    }
}
