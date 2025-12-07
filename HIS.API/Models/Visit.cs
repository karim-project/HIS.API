using System.Net.Mail;

namespace HIS.API.Models
{
    public class Visit
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public VisitType VisitType { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime AdmittedAt { get; set; }      // for outpatient, use visit time
        public DateTime? DischargedAt { get; set; }

        public ICollection<VisitNote> Notes { get; set; } = new List<VisitNote>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Guid? AdmissionId { get; set; }

        public DateTime CraetedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
