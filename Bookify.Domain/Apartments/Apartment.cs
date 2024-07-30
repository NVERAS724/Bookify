using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments
{
    public sealed class Apartment : Entity
    {
        public Apartment(Guid id) 
            : base(id)
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }
        public decimal PriceAmount { get; private set; }
        public string PriceCurrency { get; private set; }
        public decimal CleaningFeeAmount { get; private set; }
        public string CleaningFeeCurrency { get; private set; }
        public DateTime? LastBookedOnUtc { get; private set; }

        public List<Amenity> Amenities { get; private set; } = new();
    }
}
