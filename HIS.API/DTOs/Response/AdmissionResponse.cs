namespace HIS.API.DTOs.Response
{
    public class AdmissionResponse
    {
        public Guid Id { get; set; }
        public DateTime AdmissionDate { get; set; }

        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = default!;

        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; } = default!;

        public Guid BedId { get; set; }
        public string BedNumber { get; set; } = default!;
    }
}
