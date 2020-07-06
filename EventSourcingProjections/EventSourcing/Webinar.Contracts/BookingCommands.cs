using System;

namespace Webinar.Contracts
{
    public static class BookingCommands
    {
        public class Book
        {
            public string         BookingId  { get; set; }
            public string         HotelId    { get; set; }
            public string         CustomerId { get; set; }
            public DateTimeOffset From       { get; set; }
            public DateTimeOffset To         { get; set; }
        }

        public class ProcessPayment
        {
            public string BookingId { get; set; }
            public double Amount    { get; set; }
        }
    }
}
