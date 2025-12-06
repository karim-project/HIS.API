namespace HIS.API.DTOs.Request
{
    public class UpdateSpecialtyRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
