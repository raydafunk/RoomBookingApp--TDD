using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Services;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public  RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }
            var availabeRoomns = _roomBookingService.GetAvailabeRooms(bookingRequest.Date);

            if (availabeRoomns.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            }
          

            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        private static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking 
            : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email,
            };
        }
    }
}