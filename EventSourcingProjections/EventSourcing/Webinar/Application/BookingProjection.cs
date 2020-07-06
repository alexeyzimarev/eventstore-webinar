using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Webinar.Mongo;
using static Webinar.Domain.Bookings.Events;

namespace Webinar.Application
{
    public class BookingProjection
    {
        IMongoCollection<CustomerBookings> _collection;

        public BookingProjection(IMongoDatabase database) => _collection = database.For<CustomerBookings>();

        public async Task Project(object evt)
        {
            switch (evt)
            {
                case BookingCreated created:
                    await _collection.ReplaceDocument(
                        new CustomerBookings
                        {
                            Id         = created.BookingId,
                            CustomerId = created.CustomerId,
                            Bookings = new List<CustomerBookings.Booking>
                            {
                                new CustomerBookings.Booking
                                {
                                    BookingId = created.BookingId, HotelId = created.HotelId, Paid = false
                                }
                            }
                        }
                    );
                    break;
                case BookingFullyPaid e: break;
            }
        }
    }

    public class CustomerBookings : Document
    {
        public string        CustomerId { get; set; }
        public List<Booking> Bookings   { get; set; } = new List<Booking>();
        public double        Amount     { get; set; }

        public class Booking
        {
            public string         BookingId { get; set; }
            public string         HotelId   { get; set; }
            public DateTimeOffset From      { get; set; }
            public bool           Paid      { get; set; }
        }
    }
}
