using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using TravelAgent.Contracts;

namespace TravelAgent.Api
{
    [ApiController]
    [Route("/booking")]
    public class BookingQueryApi
    {
        readonly Func<IAsyncDocumentSession> _getSession;
        public BookingQueryApi(Func<IAsyncDocumentSession> getSession) => _getSession = getSession;

        [HttpGet]
        [Route("customer/bookings")]
        public Task<ICollection<BookingQueries.GetCustomerBookings.Result>> Get([FromBody] BookingQueries.GetCustomerBookings query)
        {
            return _getSession.Get(query);
        }
    }
}
