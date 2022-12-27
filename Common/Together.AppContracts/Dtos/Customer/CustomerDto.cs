﻿namespace Together.AppContracts.Dtos.Customer
{
    public class CustomerDto : BaseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
