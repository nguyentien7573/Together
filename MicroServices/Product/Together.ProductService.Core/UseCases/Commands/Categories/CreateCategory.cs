using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Category;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.UseCases.Commands.Categories
{
    public class CreateCategory
    {
        public record Command : ICreateCommand<Command.CreateCategoryModel, CategoryDto>
        {
            public CreateCategoryModel Model { get; init; } = default!;

            public record CreateCategoryModel(string Name, string Description ,bool IsActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<CategoryDto>>
            {
                private readonly IRepository<Category> _categoryRepository;

                public Handler(IRepository<Category> categoryRepository)
                {
                    _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
                }

                public async Task<ResultModel<CategoryDto>> Handle(Command request, CancellationToken cancellationToken)
                {
                    var created = await _categoryRepository.AddAsync(
                        Category.Create(
                            request.Model.Name,
                            request.Model.Description,
                            request.Model.IsActive));

                    return ResultModel<CategoryDto>.Create(new CategoryDto
                    {
                        Id = created.Id,
                        Name = created.Name,
                        Description = created.Description,
                        IsActive = created.IsActive,
                        CreatedOn = created.CreatedOn,
                        UpdatedOn = created.UpdatedOn,
                    });
                }
            }
        }
    }
}
