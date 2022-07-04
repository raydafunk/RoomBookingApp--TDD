using System;
using Xunit;
using Shouldly;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core.Test.BookingTest
{
    public class RoomBookingRequestProcessorTest
    {
        [Fact]
        public void Should_Return_Room_Booking_Reqesponse_with_Request_Values()
        {
            var bookingRequest = new RoomBookingRequest
            {
                FullName = "Test-Name",
                Email  ="Test@gmail.com",
                Date = new DateTime(2022, 6,4)
            };

            var processor = new RoomBookingRequestProcessor();

            RoomBookingResult result = processor.BookRoom(bookingRequest);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(result.FullName);
            result.Email.ShouldBe(result.Email);
            result.Date.ShouldBe(result.Date);

        }
    }
}
