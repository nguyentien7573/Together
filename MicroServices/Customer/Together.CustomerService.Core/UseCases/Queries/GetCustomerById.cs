using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Customer;
using Together.AppContracts.Dtos.Product;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.CustomerService.Core.Entities;

namespace Together.CustomerService.Core.UseCases.Queries
{
    public class GetCustomerById
    {
        public record Query : IItemQuery<Guid, CustomerDto>
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

            internal class Handler : IRequestHandler<Query, ResultModel<CustomerDto>>
            {
                private readonly IRepository<Customer> _customerRepository;

                public Handler(IRepository<Customer> customerRepository)
                {
                    _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                }

                public async Task<ResultModel<CustomerDto>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var customer = await _customerRepository.FindById(request.Id);

                    if (customer == null)
                    {
                        return null;
                    }

                    return ResultModel<CustomerDto>.Create(new CustomerDto
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                    });
                }
            }
        }
    }
}
