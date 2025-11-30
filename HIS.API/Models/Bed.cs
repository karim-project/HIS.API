namespace HIS.API.Models
{
    public class Bed
    {
        public Guid Id { get; set; }
        public string BedNumber { get; set; } = default!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = default!;
        public bool IsOccupied { get; set; }
        public Guid? CurrentAdmissionId { get; set; }
        public Admission? CurrentAdmission { get; set; }
    }
}
