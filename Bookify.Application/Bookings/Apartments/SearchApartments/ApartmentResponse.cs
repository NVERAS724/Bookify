namespace Bookify.Application.Bookings.Apartments.SearchApartments
{
    public sealed class ApartmentResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public ApartmentResponse(string name, string description, decimal price, string currency, AddressResponse address)
        {
            Name = name;
            Description = description;
            Price = price;
            Currency = currency;
            Address = address;
        }

        public string Description { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
        public AddressResponse Address { get; set; }
    }
}