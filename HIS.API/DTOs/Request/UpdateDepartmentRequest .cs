namespace HIS.API.DTOs.Request
{
    public class UpdateDepartmentRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
