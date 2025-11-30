namespace HIS.API.DTOs.Request
{
    public class UpdateDoctorRequest
    {
        public string EmployeeNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public FormFile? Img { get; set; }
        public Guid? DepartmentId { get; set; }

        


    }
}
