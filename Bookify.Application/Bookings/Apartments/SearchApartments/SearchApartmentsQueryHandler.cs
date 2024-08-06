﻿using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.Apartments.SearchApartments
{
    internal sealed class SearchApartmentsQueryHandler : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
    {
        private static readonly int[] ActiveBookingStatuses =
        {
        (int) BookingStatus.Reserved,
        (int) BookingStatus.Confirmed,
        (int) BookingStatus.Completed
    };

        private readonly ISqlConnectionFactory _connectionFactory;

        public SearchApartmentsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {

            if(request.StartDate > request.EndDate)
            {
                return new List<ApartmentResponse>();
            }

            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            SELECT 
            a.id as Id, 
            a.name as Name, 
            a.description as Description, 
            a.price_amount as Price, 
            a.price_currency as Currency, 
            a.address_country as Country,
            a.address_state as State, 
            a.address_zip_code as ZipCode, 
            a.address_city as City, 
            a.address_street as Street
            FROM Apartments AS a 
            Where not exists 
            (   
                SELECT 1 
                FROM bookings as b
                WHERE 
                    b.apartment_id = a.id AND  
                    b.duration_start <= @EndDate AND    
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveBookingStatuses)
            )
        """;

            var apartments = await connection
                .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                    sql,
                    (apartment, address) =>
                    {
                        apartment.Address = address;
                        return apartment;
                    },
                    new
                    {
                        request.StartDate,
                        request.EndDate,
                        ActiveBookingStatuses
                    },
                    splitOn: "Country");

            return apartments.ToList();


        }
    }
}
