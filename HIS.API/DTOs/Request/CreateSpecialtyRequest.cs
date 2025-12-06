namespace HIS.API.DTOs.Request
{
    public class CreateSpecialtyRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
