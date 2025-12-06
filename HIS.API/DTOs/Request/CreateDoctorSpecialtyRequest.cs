namespace HIS.API.DTOs.Request
{
    public class CreateDoctorSpecialtyRequest
    {
        public Guid DoctorId { get; set; }
        public Guid SpecialtyId { get; set; }
    }
}
