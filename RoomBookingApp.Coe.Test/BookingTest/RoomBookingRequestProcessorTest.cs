using System;
using Xunit;
using Shouldly;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Core.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Domain.Entites;

namespace RoomBookingApp.Core.Test.BookingTest
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequestProcessor _processor;

        private readonly RoomBookingRequest _bookingRequest;
        private readonly Mock<IRoomBookingService> _roomBookingServiceMock;
        private readonly List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            _bookingRequest = new RoomBookingRequest
            {
                FullName = "Test-Name",
                Email = "Test@gmail.com",
                Date = new DateTime(2022, 6, 4)
            };
            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(q => q.GetAvailabeRooms(_bookingRequest.Date))
                .Returns(_availableRooms);
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }

        [Fact]
        public void Should_Return_Room_Booking_Reqesponse_with_Request_Values()
        {



            RoomBookingResult result = _processor.BookRoom(_bookingRequest);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(result.FullName);
            result.Email.ShouldBe(result.Email);
            result.Date.ShouldBe(result.Date);

        }

        [Fact]
        public void Should_Throw_Excecption_For_Null_Request()
        {
            var excepetion = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null!));
            excepetion.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null!;
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_bookingRequest);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(savedBooking.FullName);
            savedBooking.Email.ShouldBe(savedBooking.Email);
            savedBooking.Date.ShouldBe(savedBooking.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_If_None_Avaiable()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_bookingRequest);
            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_ReturnSuccessFailure_Flag_In_Result(BookingResultFlag bookingSucesssFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();

            }

            var result = _processor.BookRoom(_bookingRequest);
            bookingSucesssFlag.ShouldBe(result.Flag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null,false)]
        public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();

            }
            else
            {
                _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
               .Callback<RoomBooking>(booking =>
               {
                   booking.Id = roomBookingId!.Value;
               });

                var result = _processor.BookRoom(_bookingRequest);
                result.RoomBookingId.ShouldBe(roomBookingId);
            }   
        }

    }
}
