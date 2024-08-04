namespace Bookify.Application.Bookings.Apartments.SearchApartments
{
    public sealed class AddressResponse
    {


        public AddressResponse(
            string country, 
            string state, 
            string zipCode, 
            string city, 
            string street)
        {
            Country = country;
            State = state;
            ZipCode = zipCode;
            City = city;
            Street = street;
        }
        public string Country { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
    }
}