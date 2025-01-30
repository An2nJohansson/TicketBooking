
namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
        {
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null) 
            {
                throw new ArgumentNullException(nameof(request));
            }
            
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