namespace HIS.API.DTOs.Request
{
    public class CreateBedRequest
    {

        public string BedNumber { get; set; } = default!;

        public Guid RoomId { get; set; }
        public bool IsOccupied { get; set; }
    }
}
