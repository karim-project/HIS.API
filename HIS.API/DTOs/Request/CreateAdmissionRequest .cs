using System.Text.Json.Serialization;

namespace HIS.API.DTOs.Request
{
    public class CreateAdmissionRequest
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public DateTime AdmitAt { get; set; }
        public DateTime? DischargeAt { get; set; }
        public Guid RoomId { get; set; }
        public Guid BedId { get; set; }


        public List<Visit>? Visits { get; set; }
    }
}
