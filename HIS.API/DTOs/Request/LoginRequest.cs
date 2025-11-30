using System.ComponentModel.DataAnnotations;

namespace HIS.API.DTOs.Request
{
    public class LoginRequest
    {
        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
