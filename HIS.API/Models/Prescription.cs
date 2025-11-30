namespace HIS.API.Models
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public Visit Visit { get; set; } = default!;
        public Guid PrescribedById { get; set; }   // Doctor
        public DateTime PrescribedAt { get; set; }

        public string? Notes { get; set; }

        // Prescription items (many-to-many with Medication but with extra fields)
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    
}
}
