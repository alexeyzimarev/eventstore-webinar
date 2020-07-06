using System;
using System.Threading.Tasks;
using Webinar.Contracts;
using Webinar.Domain.Bookings;
using Webinar.Library;

namespace Webinar.Application
{
    public class BookingCommandService
    {
        public BookingCommandService(IAggregateStore store) => Store = store;

        IAggregateStore Store { get; }

        public Task Handle(BookingCommands.Book cmd)
        {
            var booking = new Booking(cmd.BookingId);
            booking.CreateBooking(cmd.HotelId, cmd.CustomerId, cmd.From, cmd.To);
            return Store.Store(booking);
        }

        public Task Handle(BookingCommands.ProcessPayment cmd) => HandleUpdate<Booking>(cmd.BookingId, b => b.ProcessPayment(cmd.Amount));

        async Task HandleUpdate<T>(string aggregateId, Action<T> operation) where T : Aggregate
        {
            var aggregate = await Store.Load<T>(aggregateId);
            if (aggregate == null) throw new InvalidOperationException($"Entity with id {aggregateId} cannot be found");

            operation(aggregate);
            await Store.Store(aggregate);
        }
    }
}
