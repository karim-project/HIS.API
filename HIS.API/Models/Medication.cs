namespace HIS.API.Models
{
    public class Medication
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Brand { get; set; }
        public string? Form { get; set; }    // tablet, syrup...
        public string? Strength { get; set; } // 500mg
        public string? Code { get; set; }     // internal code / barcode

        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
        public ICollection<PharmacyStock> Stocks { get; set; } = new List<PharmacyStock>();
    }
}
