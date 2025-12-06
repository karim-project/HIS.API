namespace HIS.API.DTOs.Response
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = default!;

        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = default!;
    }
}
