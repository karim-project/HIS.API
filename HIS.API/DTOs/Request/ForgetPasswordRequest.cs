using System.ComponentModel.DataAnnotations;

namespace HIS.API.DTOs.Request
{
    public class ForgetPasswordRequest
    {
        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;
    }
}
