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

            public record UpdateProductModel(Guid Id, string Name, string Description, string SKU, Guid CategoryId, Guid? InventoryId, decimal Price, Guid? DiscountId, bool IsActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

                    RuleFor(x => x.Model.CategoryId)
                 .NotEmpty().WithMessage("CategoryId is required.");

                    RuleFor(x => x.Model.InventoryId)
                        .NotEmpty().WithMessage("InventoryId is required.");
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
                    prod.Description = request.Model.Description;
                    prod.SKU = request.Model.SKU;
                    prod.CategoryId = request.Model.CategoryId;
                    prod.InventoryId = request.Model.InventoryId ?? new Guid();
                    prod.Price = request.Model.Price;
                    prod.DiscountId = request.Model.DiscountId ?? new Guid();
                    prod.IsActive = request.Model.IsActive;

                    var result = await _productRepository.Update(prod);

                    return result;
                }
            }
        }
    }
}
