using FluentValidation;
using MediatR;
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Commands.Products
{
    public class DeleteProduct
    {
        public record Command : IRequest<bool>
        {
            public Guid Id { get; init; } = default!;
            public record DeleteProductModel(Guid Id);

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
                private readonly IProductRepository<Product> _productRepository;

                public Handler(IProductRepository<Product> productRepository)
                {
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }
                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    if (request == null)
                        return false;

                    var prod = await _productRepository.FindById(request.Id);
                    
                    if (prod == null) 
                        return false;

                    prod.Active = false;

                    return await _productRepository.Update(prod);
                   
                }
            }
        }
    }
}
