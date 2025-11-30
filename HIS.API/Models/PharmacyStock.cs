namespace HIS.API.Models
{
    public class PharmacyStock
    {
        public Guid Id { get; set; }
        public Guid MedicationId { get; set; }
        public Medication Medication { get; set; } = default!;
        public int QuantityAvailable { get; set; }
        public string? Location { get; set; } // which pharmacy / shelf
        public DateTime LastUpdated { get; set; }
    }
}
