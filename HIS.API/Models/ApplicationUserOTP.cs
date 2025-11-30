namespace HIS.API.Models
{
    public class ApplicationUserOTP
    {
        public string Id { get; set; }
        public string OTP { get; set; }


        public DateTime CreateAt { get; set; }
        public DateTime ValidTO { get; set; }
        public bool IsValid { get; set; }


        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
