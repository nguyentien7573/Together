using MediatR;
using Together.Products.Application.Commands;
using Together.Products.Application.Mappers;
using Together.Products.Application.Responses;
using Together.Products.Core.Entities;
using Together.Products.Core.Repositories;

namespace Together.Products.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var pro = MapperConfig.Mapper.Map<Product>(request);

            if (pro is null)
            {
                throw new ApplicationException("There is an issue with mapping");
            }

            var newPro = await _productRepository.AddAsync(pro);
            var proResponse = MapperConfig.Mapper.Map<ProductResponse>(newPro);
            return proResponse;
        }
    }
}
