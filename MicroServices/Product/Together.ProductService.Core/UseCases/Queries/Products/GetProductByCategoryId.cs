using AutoMapper;
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
using Together.ProductService.Core.Entities;
using Together.ProductService.Core.EntityConfig;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Core.UseCases.Queries.Products
{
    public class GetProductByCategoryId
    {
        public record Query : IRequest<IEnumerable<ProductDto>>
        {
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

            internal class Handler : IRequestHandler<Query, IEnumerable<ProductDto>>
            {
                private readonly IProductRepository<Product> _productRepository;

                public Handler(IProductRepository<Product> productRepository)
                {
                    _productRepository = productRepository;
                }

                public async Task<IEnumerable<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var prods = await _productRepository.GetProductsByCategoryId(request.Id);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new MappingProfile());
                    });
                    
                    var mapper = config.CreateMapper();
                    
                    var result = mapper.Map<IEnumerable<Product>, List<ProductDto>>(prods);

                    return result;

                }
            }
        }
    }
}
