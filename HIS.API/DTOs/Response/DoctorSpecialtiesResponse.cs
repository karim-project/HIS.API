namespace HIS.API.DTOs.Response
{
    public class DoctorSpecialtiesResponse
    {
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid SpecialtyId { get; set; }
        public string SpecialtyName { get; set; } = null!;
    }
}
