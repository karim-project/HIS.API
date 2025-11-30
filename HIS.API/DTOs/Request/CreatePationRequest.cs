using System.ComponentModel.DataAnnotations;

namespace HIS.API.DTOs.Request
{
    public class CreatePationRequest
    {
        public string MedicalRecordNumber { get; set; } = default!;
        [Required]
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
     
    }
}
