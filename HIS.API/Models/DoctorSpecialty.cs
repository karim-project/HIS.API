namespace HIS.API.Models
{
    public class DoctorSpecialty
    {
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = default!;
    }
}
