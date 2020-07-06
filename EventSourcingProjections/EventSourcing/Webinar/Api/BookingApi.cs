using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webinar.Application;
using Webinar.Contracts;

namespace Webinar.Api
{
    [ApiController]
    [Route("/booking")]
    public class BookingApi
    {
        readonly BookingCommandService _commandService;
        
        public BookingApi(BookingCommandService commandService) => _commandService = commandService;

        [HttpPost]
        [Route("book")]
        public async Task<string> Book([FromBody] BookingCommands.Book cmd)
        {
            await _commandService.Handle(cmd);
            return "Done";
        }

        [HttpPost]
        [Route("pay")]
        public async Task<string> ConfirmPayment([FromBody] BookingCommands.ProcessPayment cmd)
        {
            await _commandService.Handle(cmd);
            return "Nice!";
        }
    }
}
