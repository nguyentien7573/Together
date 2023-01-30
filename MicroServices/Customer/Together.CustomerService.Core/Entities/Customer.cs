using Together.Core.Domain;

namespace Together.CustomerService.Core.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; protected set; } = default!;
        public string LastName { get; protected set; } = default!;
        public string Email { get; protected set; } = default!;

        public static Customer Create(string firstname, string lastname, string email)
        {
            return Create(Guid.NewGuid(), firstname, lastname, email);
        }

        public static Customer Create(Guid id, string firstname, string lastname, string email)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");
          
            Customer customer = new()
            {
                Id = id,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
            };

            return customer;
        }
    }
}
