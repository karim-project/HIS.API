namespace HIS.API.Models
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = default!;
        public string BlobPath { get; set; } = default!; // storage reference (S3/Azure)
        public Guid? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public Guid? VisitId { get; set; }
        public Visit? Visit { get; set; }
        public DateTime UploadedAt { get; set; }
        public Guid UploadedById { get; set; }
    }
}
