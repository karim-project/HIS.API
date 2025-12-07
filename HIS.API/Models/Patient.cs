using System.ComponentModel.DataAnnotations;

namespace HIS.API.Models
{
    public enum Gender {  Male = 1, Female = 2}
    public enum AppointmentStatus { Scheduled, Confirmed, CheckedIn, Completed, Cancelled, NoShow }
    public enum VisitType { Outpatient, Inpatient, Emergency }
    public enum InvoiceStatus { Draft, Issued, Paid, Cancelled, Refunded }

    public class Patient
    {
        public Guid Id { get; set; }
        public string MedicalRecordNumber { get; set; } = default!;
        [Required]
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Insurance>? Insurances { get; set; } = new List<Insurance>();
        public ICollection<Invoice>? Invoices  { get; set; } = new List<Invoice>();

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
