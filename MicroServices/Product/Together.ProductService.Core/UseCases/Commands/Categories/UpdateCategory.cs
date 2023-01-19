using FluentValidation;
using MediatR;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.UseCases.Commands.Categories
{
    public class UpdateCategory
    {
        public record Command : IRequest<bool>
        {
            public UpdateCategoryModel Model { get; init; } = default!;

            public record UpdateCategoryModel(Guid Id, string Name, string Description, bool IsActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
                }
            }

            internal class Handler : IRequestHandler<Command, bool>
            {
                private readonly IRepository<Category> _categoryRepository;

                public Handler(IRepository<Category> categoryRepository)
                {
                    _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    var category = await _categoryRepository.FindById(request.Model.Id);

                    if (category == null)
                    {
                        return false;
                    }

                    category.Name = request.Model.Name;
                    category.Description = request.Model.Description;
                    category.IsActive = request.Model.IsActive;

                    var updated = await _categoryRepository.Update(category);

                    return updated;
                }
            }
        }
    }
}
