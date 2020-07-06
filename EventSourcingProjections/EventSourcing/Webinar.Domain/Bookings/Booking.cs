using System;
using Webinar.Library;

namespace Webinar.Domain.Bookings
{
    public class Booking : Aggregate
    {
        string         _customerId;
        string         _hotelId;
        BookingStatus  _status;
        DateTimeOffset _from;
        DateTimeOffset _to;
        string         _id;
        bool           _paid;

        Booking() { }

        public Booking(string id) => _id = id;

        public void CreateBooking(string hotelId, string customerId, DateTimeOffset from, DateTimeOffset to)
            => Apply(
                new Events.BookingCreated
                {
                    BookingId  = _id,
                    HotelId    = hotelId,
                    CustomerId = customerId,
                    From       = from,
                    To         = to
                }
            );

        public void ProcessPayment(in double amount)
        {
            EnsureExists();

            Apply(
                new Events.BookingFullyPaid
                {
                    BookingId  = _id,
                    PaidStatus = true,
                    CustomerId = _customerId
                }
            );
        }

        protected override void When(object evt)
        {
            switch (evt)
            {
                case Events.BookingCreated e:
                    _id         = e.BookingId;
                    _hotelId    = e.HotelId;
                    _customerId = e.CustomerId;
                    _from       = e.From;
                    _to         = e.To;
                    _status     = BookingStatus.Booked;
                    break;
                case Events.BookingFullyPaid e:
                    _paid   = e.PaidStatus;
                    _status = _paid ? BookingStatus.Paid : _status;
                    break;
            }
        }

        public override string GetId() => _id;

        void EnsureExists()
        {
            if (Version == -1) throw new InvalidOperationException("Must exist");
        }
    }

    public enum BookingStatus
    {
        Booked,
        Confirmed,
        Paid,
        Cancelled
    }
}
