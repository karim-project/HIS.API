namespace HIS.API.DTOs.Request
{
    public class UpdateAppointmentRequest
    {

        public Guid DoctorId { get; set; }

        public Guid PatientId { get; set; }

        public DateTime Date { get; set; }

        public string? Notes { get; set; }
    }
}
