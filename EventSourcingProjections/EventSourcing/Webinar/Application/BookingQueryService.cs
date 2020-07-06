using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Webinar.Mongo;
using static Webinar.Contracts.BookingQueries;

namespace Webinar.Application
{
    public class BookingQueryService
    {
        readonly IMongoDatabase _database;

        public BookingQueryService(IMongoDatabase database) => _database = database;

        public async Task<ICollection<GetCustomerBookings.Result>> Get([FromBody] GetCustomerBookings query)
        {
            var result = await _database.LoadDocument<CustomerBookings>(query.CustomerId);

            return result.Bookings.Select(
                    x => new GetCustomerBookings.Result
                    {
                        BookingId = x.BookingId,
                        HotelId   = x.HotelId
                    }
                )
                .ToArray();
        }
    }

    public static class BookingQueryExtension
    {
        public static async Task<ICollection<GetCustomerBookings.Result>> Get(this Func<IAsyncDocumentSession> getSession, GetCustomerBookings query)
        {
            using var session = getSession();

            var result = await session.LoadAsync<CustomerBookings>(query.CustomerId);

            return result.Bookings.Select(
                    x => new GetCustomerBookings.Result
                    {
                        BookingId = x.BookingId,
                        HotelId   = x.HotelId
                    }
                )
                .ToArray();
        }
        
    }
}
