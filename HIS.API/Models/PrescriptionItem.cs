namespace HIS.API.Models
{
    public class PrescriptionItem
    {
        public Guid Id { get; set; }
        public Guid PrescriptionId { get; set; }
        public Prescription Prescription { get; set; } = default!;
        public Guid MedicationId { get; set; }
        public Medication Medication { get; set; } = default!;
        public string Dosage { get; set; } = default!;       // e.g. "500 mg"
        public string Frequency { get; set; } = default!;    // e.g. "Twice a day"
        public int DurationDays { get; set; }                // e.g. 7
        public int Quantity { get; set; }
    }
}
