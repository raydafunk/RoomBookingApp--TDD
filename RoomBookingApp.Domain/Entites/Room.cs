namespace RoomBookingApp.Core.Domain.Entites
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<RoomBooking>? Roombookings { get; set; }
    }
}