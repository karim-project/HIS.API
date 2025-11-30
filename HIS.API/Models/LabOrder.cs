namespace HIS.API.Models
{
    public class LabOrder
    {
        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public Visit Visit { get; set; } = default!;
        public Guid OrderedById { get; set; } // Doctor
        public DateTime OrderedAt { get; set; }
        public ICollection<LabOrderItem> LabOrderItems  { get; set; } = new List<LabOrderItem>();
        public ICollection<LabResult> Results { get; set; } = new List<LabResult>();
    }
}
