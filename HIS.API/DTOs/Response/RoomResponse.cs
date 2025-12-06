namespace HIS.API.DTOs.Response
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public int Capacity { get; set; }
        public int FloorNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
