using RoomBookingApp.Core.Domain.Entites;
using RoomBookingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Persistence.Repository
{
    public class RoomBookingService : IRoomBookingService
    {
        public IEnumerable<Room> GetAvailabeRooms(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
