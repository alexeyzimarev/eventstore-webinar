using System;

namespace Webinar.Domain.Bookings
{
    public static class Events
    {
        public class BookingCreated
        {
            public string BookingId { get; set; }
            public string HotelId { get; set; }
            public string CustomerId { get; set; }
            public DateTimeOffset From { get; set; }
            public DateTimeOffset To { get; set; }
        }

        public class BookingFullyPaid
        {
            public string BookingId { get; set; }
            public string CustomerId { get; set; }
            public bool PaidStatus { get; set; }
        }
    }
}
