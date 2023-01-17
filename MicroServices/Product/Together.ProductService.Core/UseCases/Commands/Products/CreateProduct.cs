﻿using FluentValidation;
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

            public record CreateProductModel(string Name, int Quantity, decimal Cost, Guid CategoryID, bool isActive);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

                    RuleFor(x => x.Model.Quantity)
                        .GreaterThanOrEqualTo(1).WithMessage("Quantity should at least greater than or equal to 1.");

                    RuleFor(x => x.Model.Cost)
                        .GreaterThanOrEqualTo(1000).WithMessage("Cost should be greater than 1000.");

                    RuleFor(x => x.Model.CategoryID)
                        .NotEmpty().WithMessage("Category is required.");
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

                    var cate = await _categoryRepository.FindById(request.Model.CategoryID);

                    if (cate == null)
                        return null;


                    var created = await _productRepository.AddAsync(
                        Product.Create(
                            request.Model.Name,
                            request.Model.Quantity,
                            request.Model.Cost,
                            request.Model.CategoryID,
                            request.Model.isActive));

                    return ResultModel<ProductDto>.Create(new ProductDto
                    {
                        Id = created.Id,
                        Name = created.Name,
                        Active = created.Active,
                        Cost = created.Cost,
                        Quantity = created.Quantity,
                        CategoryId = created.CategoryId,
                        CreatedOn = created.CreatedOn,
                        UpdatedOn = created.UpdatedOn,
                    });
                }
            }
        }
    }
}
