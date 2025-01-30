using Moq;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        private readonly TicketBookingRequestProcessor _processor;

        public TicketBookingRequestProcessorTests() 
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
            
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            
            var request = new TicketBookingRequest
            {
                FirstName = "Anton",
                LastName = "Johansson",
                Email = "anton@gmail.se"
            };

            // Act
            TicketBookingResponse response = _processor.Book(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
           
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

            // Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveToDataBase()
        {
            //Arrange
            TicketBooking savedTicketBooking = null;

            // Setup the Save method to capture the saved ticket booking
            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
                .Callback<TicketBooking>((ticketBooking) =>
                {
                    savedTicketBooking = ticketBooking;
                });
            
            var request = new TicketBookingRequest
            {
                FirstName = "Anton",
                LastName = "Johansson",
                Email = "anton@gmail.se"
            };

            //Act
            TicketBookingResponse response = _processor.Book(request);

            //Assert
            Assert.NotNull(savedTicketBooking);
            Assert.Equal(request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(request.LastName, savedTicketBooking.LastName);
            Assert.Equal(request.Email, savedTicketBooking.Email);

        }
    }
}