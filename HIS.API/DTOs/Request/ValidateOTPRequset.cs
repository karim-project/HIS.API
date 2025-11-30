

using System.ComponentModel.DataAnnotations;

namespace HIS.API.DTOs.Request
{
    public class ValidateOTPRequset
    {
        [Required]
        public string OTP { get; set; } = string.Empty;

        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
