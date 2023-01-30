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
    public class DeleteShoppingSessions
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; } = default!;
            public record DeleteShoppingSessionModel(Guid Id);

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
                private readonly IRepository<ShoppingSession> _shoppingSessionRepository;

                public Handler(IRepository<ShoppingSession> shoppingSessionRepository)
                {
                    _shoppingSessionRepository = shoppingSessionRepository ?? throw new ArgumentNullException(nameof(shoppingSessionRepository));
                }
                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                        return false;

                    var prod = await _shoppingSessionRepository.FindById(request.Id);

                    if (prod == null)
                        return false;

                    prod.IsActive = false;

                    return await _shoppingSessionRepository.Update(prod);
                }
            }
        }
    }
}