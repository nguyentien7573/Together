using FluentValidation;
using MediatR;
using Together.AppContracts.Dtos.Customer;
using Together.Core.Domain;
using Together.Core.Repository;
using Together.CustomerService.Core.Entities;

namespace Together.CustomerService.Core.UseCases.Commands
{
    public class CreateCustomer
    {
        public record Command : ICreateCommand<Command.CreateCustomerModel, CustomerDto>
        {
            public CreateCustomerModel Model { get; init; } = default!;

            public record CreateCustomerModel(string FirstName, string LastName, string Email, Guid CountryId);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.FirstName)
                        .NotEmpty().WithMessage("FirstName is required.")
                        .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

                    RuleFor(v => v.Model.LastName)
                        .NotEmpty().WithMessage("LastName is required.")
                        .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

                    RuleFor(v => v.Model.Email)
                        .NotEmpty().WithMessage("Email is required.")
                        .EmailAddress().WithMessage("Email should in email format.")
                        .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<CustomerDto>>
            {
                private readonly IRepository<Customer> _customerRepository;

                public Handler(IRepository<Customer> customerRepository)
                {
                    _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                }

                public async Task<ResultModel<CustomerDto>> Handle(Command request,
                    CancellationToken cancellationToken)
                {
                    
                    var customer = Customer.Create(request.Model.FirstName, request.Model.LastName, request.Model.Email);

                    //customer.AddDomainEvent(new CustomerCreatedIntegrationEvent());

                    var created = await _customerRepository.AddAsync(customer);

                    return ResultModel<CustomerDto>.Create(new CustomerDto
                    {
                        Id = created.Id,
                        FirstName = created.FirstName,
                        LastName = created.LastName,
                        Email = created.Email
                    });
                }
            }
        }
    }
}
