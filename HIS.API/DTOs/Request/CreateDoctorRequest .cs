namespace HIS.API.DTOs.Request
{
    public class CreateDoctorRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }   
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // لو عندك specialties للطبيب
        public List<int>? SpecialtyIds { get; set; }
        public IFormFile? Img { get; set; }
    }
}
