using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.AppContracts.Dtos.Shopping.CartItem;
using Together.AppContracts.Dtos.Shopping.Shopping;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Core.UseCases.Commands.ShoppingSessions
{
    public class InsertShoppingSession
    {
        public record Command : ICreateCommand<Command.ShoppingSessionModel, ShoppingSessionDto>
        {
            public ShoppingSessionModel Model { get; init; } = default!;

            public record ShoppingSessionModel(Guid CustomerId, decimal Total, List<CartItem> CartItems);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.CustomerId)
                        .NotEmpty().WithMessage("CustomerId is required.");
                    RuleFor(v => v.Model.Total)
                       .NotEmpty().WithMessage("Total is bigger than 0.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<ShoppingSessionDto>>
            {
                private readonly IRepository<ShoppingSession> _shoppingSessionRepository;

                public Handler(IRepository<ShoppingSession> shoppingSessionRepository)
                {
                    _shoppingSessionRepository = shoppingSessionRepository ?? throw new ArgumentNullException(nameof(shoppingSessionRepository));
                }

                public async Task<ResultModel<ShoppingSessionDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var created = await _shoppingSessionRepository.AddAsync(
                        ShoppingSession.Create(
                            request.Model.CustomerId,
                            request.Model.Total,
                            request.Model.CartItems));

                    return ResultModel<ShoppingSessionDto>.Create(new ShoppingSessionDto
                    {
                        Id = created.Id,
                        CustomerId = created.CustomerId,
                        Total = request.Model.Total,
                        CreatedOn = created.CreatedOn,
                        UpdatedOn = created.UpdatedOn,
                    });
                }
            }
        }
    }
}
