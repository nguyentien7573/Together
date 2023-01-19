using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Product;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Commands.Products
{
    public class CreateProduct
    {
        public record Command : ICreateCommand<Command.CreateProductModel, ProductDto>
        {
            public CreateProductModel Model { get; init; } = default!;

            public record CreateProductModel(string Name, string Description, string SKU, Guid CategoryId, Guid? InventoryId, decimal Price, Guid? DiscountId, bool IsActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

                    RuleFor(x => x.Model.CategoryId)
                        .NotEmpty().WithMessage("CategoryId is required.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<ProductDto>>
            {
                private readonly IProductRepository<Product> _productRepository;
                private readonly IRepository<Category> _categoryRepository;

                public Handler(IProductRepository<Product> productRepository, IRepository<Category> categoryRepository)
                {
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                    _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
                }

                public async Task<ResultModel<ProductDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                        return null;

                    var cate = await _categoryRepository.FindById(request.Model.CategoryId);

                    if (cate == null)
                        return null;


                    var created = await _productRepository.AddAsync(
                        Product.Create(
                            request.Model.Name,
                            request.Model.Description,
                            request.Model.SKU,
                            request.Model.CategoryId,
                            request.Model.InventoryId ?? new Guid(),
                            request.Model.Price,
                            request.Model.DiscountId ?? new Guid(),
                            request.Model.IsActive));

                    return ResultModel<ProductDto>.Create(new ProductDto
                    {
                        Id = created.Id,
                        Name = created.Name,
                       
                    });
                }
            }
        }
    }
}
