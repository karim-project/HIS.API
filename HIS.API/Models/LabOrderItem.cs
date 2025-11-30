namespace HIS.API.Models
{
    public class LabOrderItem
    {
        public Guid Id { get; set; }
        public Guid LabOrderId { get; set; }
        public LabOrder LabOrder { get; set; } = default!;
        public string TestCode { get; set; } = default!;
        public string TestName { get; set; } = default!;
        public string? Specimen { get; set; }
    }
}
