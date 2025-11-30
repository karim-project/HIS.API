namespace HIS.API.Models
{
    public class LabResult
    {
        public Guid Id { get; set; }
        public Guid LabOrderItemId { get; set; }
        public LabOrderItem LabOrderItem { get; set; } = default!;
        public string ResultValue { get; set; } = default!;
        public string? NormalRange { get; set; }
        public string? Units { get; set; }
        public DateTime ResultedAt { get; set; }
        public Guid? PerformedById { get; set; }
    }
}
