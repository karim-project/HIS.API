namespace HIS.API.DTOs.Response
{
    public class DepartmentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
