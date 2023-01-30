using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Core.Repository;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Core.UseCases.Commands.ShoppingSessions
{
    public class UpdateShoppingSessions
    {
        public record Command : IRequest<bool>
        {
            public UpdateShoppingSessionModel Model { get; init; } = default!;

            public record UpdateShoppingSessionModel(Guid Id, Guid CustomerId, decimal Total, List<CartItem> CartItems);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Id)
                        .NotEmpty().WithMessage("Id is required.");

                    RuleFor(x => x.Model.CustomerId)
                        .NotEmpty().WithMessage("CustomerId is required.");

                    RuleFor(x => x.Model.Total)
                        .NotEmpty().WithMessage("Quantity is bigger than 0.");
                }
            }

            internal class Handler : IRequestHandler<Command, bool>
            {
                private readonly IRepository<ShoppingSession> _shoppingSessionRepository;

                public Handler(IRepository<ShoppingSession> shoppingSessionRepository)
                {
                    _shoppingSessionRepository = shoppingSessionRepository;
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                    {
                        return false;
                    }

                    var session = await _shoppingSessionRepository.FindById(request.Model.Id);
                    if (session == null)
                    {
                        return false;
                    }

                    session.CustomerId = request.Model.CustomerId;
                    session.Total = request.Model.Total;
                    session.CartItems = request.Model.CartItems;

                    var result = await _shoppingSessionRepository.Update(session);

                    return result;
                }
            }
        }
    }
}
