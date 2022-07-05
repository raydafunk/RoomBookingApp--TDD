﻿using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain.Entites;
using RoomBookingApp.Persistence.Repository;
using System;
using System.Linq;
using Xunit;

namespace RoomBookingApp.Persistence.Test.Services
{
    public class RoomBookingServiceTest
    {

        [Fact]
        public void should_Return_Available_Rooms()
        {
            //Arrange
            var date = new DateTime(2022, 07, 05);

            var dbContextopitons = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                .UseInMemoryDatabase("AvailableRoomTest")
                .Options;

            using var context = new RoomBookingAppDbContext(dbContextopitons);
            context.Add(new Room { Id = 1, Name = "Room1" });
            context.Add(new Room { Id = 2, Name = "Room2" });
            context.Add(new Room { Id = 3, Name = "Room3" });

            context.Add(new RoomBooking { RoomId = 1, Date = date });
            context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //Act
            var avaialbleRooms = roomBookingService.GetAvailabeRooms(date);

            //assert
            Assert.Equal(2, avaialbleRooms.Count());
            Assert.Contains(avaialbleRooms, q => q.Id == 2);
            Assert.Contains(avaialbleRooms, q => q.Id == 3);
            Assert.DoesNotContain(avaialbleRooms, q => q.Id == 4);
            Assert.Contains(avaialbleRooms, q => q.Name == "Room1");
            Assert.Contains(avaialbleRooms, q => q.Name == "Room2");
            Assert.Contains(avaialbleRooms, q => q.Name == "Room3");
            Assert.DoesNotContain(avaialbleRooms, q => q.Name  == "Room4");
        }
    }
}
