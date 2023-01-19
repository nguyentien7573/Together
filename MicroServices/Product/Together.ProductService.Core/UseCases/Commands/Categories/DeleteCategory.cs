using FluentValidation;
using MediatR;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Commands.Categories
{
    public class DeleteCategory
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; } = default!;

            public record UpdateCategoryModel(Guid Id);

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
                private readonly IRepository<Category> _categoryRepository;
                private readonly IProductRepository<Product> _productRepository;

                public Handler(IRepository<Category> categoryRepository, IProductRepository<Product> productRepository)
                {
                    _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    var category = await _categoryRepository.FindById(request.Id);

                    if (category == null)
                    {
                        return false;
                    }

                    category.IsActive = false;
                    var updated = await _categoryRepository.Update(category);

                    var prods = await _productRepository.GetProductsByCategoryId(category.Id);

                    if (updated)
                    {
                        foreach (var item in prods)
                        {
                            item.IsActive = false;
                            await _productRepository.Update(item);
                        }
                    }

                    return updated;
                }
            }
        }
    }
}
