namespace HIS.API.Models
{
    public class Admission
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        public Patient Patient { get; set; } = default!;
        public DateTime AdmitAt { get; set; }
        public DateTime? DischargeAt { get; set; }
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }
        public Guid BedId { get; set; }
        public Bed? Bed { get; set; }



       
        public string? Reason { get; set; }

        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }

}
