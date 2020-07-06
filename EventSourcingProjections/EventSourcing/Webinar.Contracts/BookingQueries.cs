using System;

namespace Webinar.Contracts
{
    public static class BookingQueries
    {
        public class GetCustomerBookings
        {
            public string CustomerId { get; set; }

            public class Result
            {
                public string         BookingId { get; set; }
                public string         HotelId   { get; set; }
                public DateTimeOffset From      { get; set; }
                public bool           Paid      { get; set; }
            }
        }
    }
}
