namespace HIS.API.DTOs.Request
{
    public class UpdateRoomRequest
    {
        public string Number { get; set; } = null!;
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
