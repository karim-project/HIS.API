namespace HIS.API.DTOs.Response
{
    public class BedResponse
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = default!;
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; } = default!;
    }
}
