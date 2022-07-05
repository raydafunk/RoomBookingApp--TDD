﻿using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Domain.Entites
{
    public class RoomBooking: RoomBookingBase
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }

    }
}