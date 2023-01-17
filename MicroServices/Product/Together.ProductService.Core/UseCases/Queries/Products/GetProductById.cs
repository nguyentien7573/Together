using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Product;
using Together.Core.Domain;
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Queries.Products
{
    public class GetProductById
    {
        public record Query : IItemQuery<Guid, ProductDto>
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

            internal class Handler : IRequestHandler<Query, ResultModel<ProductDto>>
            {
                private readonly IProductRepository<Product> _productRepository;

                public Handler(IProductRepository<Product> productRepository)
                {
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }

                public async Task<ResultModel<ProductDto>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var product = await _productRepository.FindById(request.Id);

                    if (product == null)
                    {
                        return null;
                    }

                    return ResultModel<ProductDto>.Create(new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Active = product.Active,
                        Cost = product.Cost,
                        Quantity = product.Quantity,
                        CreatedOn = product.CreatedOn,
                        UpdatedOn = product.UpdatedOn,
                    });
                }
            }
        }
    }
}
