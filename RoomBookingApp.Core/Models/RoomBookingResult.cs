using RoomBookingApp.Core.Enums;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public int? RoomBookingId { get; set; }
        public BookingResultFlag Flag { get; set; }
    }
}