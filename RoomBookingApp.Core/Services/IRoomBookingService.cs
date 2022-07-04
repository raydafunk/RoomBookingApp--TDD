using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Core.Services
{
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);
    }
}
