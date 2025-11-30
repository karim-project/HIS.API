namespace HIS.API.Models
{
    public class Insurance
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public string ProviderName { get; set; } = default!;
        public string PolicyNumber { get; set; } = default!;
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
