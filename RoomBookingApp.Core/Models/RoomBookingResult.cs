using RoomBookingApp.Core.Enums;
using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public int? RoomBookingId { get; set; }
        public BookingResultFlag Flag { get; set; }
    }
}