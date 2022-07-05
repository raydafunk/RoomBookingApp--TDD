using RoomBookingApp.Core.Domain.Entites;
using RoomBookingApp.Core.Services;

namespace RoomBookingApp.Persistence.Repository
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
          this._context = context;
        }

        public IEnumerable<Room> GetAvailabeRooms(DateTime date)
        {
            return _context.Rooms.Where(q => !q.Roombookings!.Any(x => x.Date == date)).ToList();

        }

        public void Save(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
