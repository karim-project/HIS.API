namespace HIS.API.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; } = default!;
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public InvoiceStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime IssuedAt { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
      
    }
}
