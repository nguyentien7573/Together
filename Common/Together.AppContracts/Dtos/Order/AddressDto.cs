namespace Together.AppContracts.Dtos.Order
{
    public class AddressDto
    {
        public string Street { get; private set; } = default!;
        public string City { get; private set; } = default!;
        public string State { get; private set; } = default!;
        public string Country { get; private set; } = default!;
        public string ZipCode { get; private set; } = default!;
    }
}
