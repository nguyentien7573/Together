using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.AppContracts.Dtos.Category;
using Together.AppContracts.Dtos.Product;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.UseCases.Queries.Categories
{
    public class GetCategoryById
    {
        public record Query : IItemQuery<Guid, CategoryDto>
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

            internal class Handler : IRequestHandler<Query, ResultModel<CategoryDto>>
            {
                private readonly IRepository<Category> _categoryRepository;

                public Handler(IRepository<Category> categoryRepository)
                {
                    _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
                }

                public async Task<ResultModel<CategoryDto>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var product = await _categoryRepository.FindById(request.Id);

                    if (product == null)
                    {
                        return null;
                    }

                    return ResultModel<CategoryDto>.Create(new CategoryDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        IsActive = product.IsActive,
                        CreatedOn = product.CreatedOn,
                        UpdatedOn = product.UpdatedOn,
                    });
                }
            }
        }
    }
}
