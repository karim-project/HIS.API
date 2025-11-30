namespace HIS.API.DTOs.Request
{
    public class TokenApiRequest
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
