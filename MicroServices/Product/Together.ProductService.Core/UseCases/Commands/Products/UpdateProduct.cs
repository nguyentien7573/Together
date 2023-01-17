using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Product;
using Together.Core.Domain;
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Commands.Products
{
    public class UpdateProduct
    {
        public record Command : IRequest<bool>
        {
            public UpdateProductModel Model { get; init; } = default!;

            public record UpdateProductModel(Guid Id, string Name, int Quantity, decimal Cost, Guid CategoryID, bool isActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

                    RuleFor(x => x.Model.Quantity)
                        .GreaterThanOrEqualTo(1).WithMessage("Quantity should at least greater than or equal to 1.");

                    RuleFor(x => x.Model.Cost)
                        .GreaterThanOrEqualTo(1000).WithMessage("Cost should be greater than 1000.");

                    RuleFor(x => x.Model.CategoryID)
                        .NotEmpty().WithMessage("Category is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, bool>
            {
                private readonly IProductRepository<Product> _productRepository;

                public Handler(IProductRepository<Product> productRepository)
                {
                    _productRepository = productRepository;
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                    {
                        return false;
                    }
                    
                    var prod = await _productRepository.FindById(request.Model.Id);
                    if (prod == null)
                    {
                        return false;
                    }

                    prod.Name = request.Model.Name;
                    prod.Quantity = request.Model.Quantity;
                    prod.Cost = request.Model.Cost;
                    prod.CategoryId = request.Model.CategoryID;
                    prod.Active = request.Model.isActive;

                    var result = await _productRepository.Update(prod);

                    return result;
                }
            }
        }
    }
}
