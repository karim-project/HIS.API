using System.ComponentModel.DataAnnotations;

namespace HIS.API.DTOs.Request
{
    public class ResendComfirmEmailRequset
    {
        [Required]
        public string UserNameOrEmail { get; set; } = string.Empty;
    }
}
