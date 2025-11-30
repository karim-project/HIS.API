namespace HIS.API.DTOs.Response
{
    public class DoctorResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Img { get; set; }

        // Department
        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        // Specialties
        public List<DoctorSpecialtyResponse> Specialties { get; set; } = new();
    }
}
