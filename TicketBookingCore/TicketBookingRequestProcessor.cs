
using System.Xml.Linq;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        private readonly ITicketBookingRepository _ticketBookingRepository;
        public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
        {
            _ticketBookingRepository = ticketBookingRepository;
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            

            if (request is null) 
            {
                throw new ArgumentNullException(nameof(request));
            }

            var ticketBooking = new TicketBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName, 
                Email = request.Email
            };
            _ticketBookingRepository.Save(ticketBooking);

            
            //refractor för att returnera en ny TicketBookingResponse
            return new TicketBookingResponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            
            
        }
    }
}