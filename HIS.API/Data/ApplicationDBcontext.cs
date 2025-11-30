using HIS.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HIS.API.Data
{
    public class ApplicationDBcontext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options)
      : base(options)
        {
        }

        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Insurance> Insurances  { get; set; }
        public DbSet<Invoice> Invoices  { get; set; }
        public DbSet<InvoiceItem> InvoiceItems  { get; set; }
        public DbSet<LabOrderItem> LabOrderItems  { get; set; }
        public DbSet<LabOrder> labOrders  { get; set; }
        public DbSet<LabResult> LabResults   { get; set; }
        public DbSet<Medication> Medications   { get; set; }
        public DbSet<Patient> Patients   { get; set; }
        public DbSet<PharmacyStock> PharmacyStocks    { get; set; }
        public DbSet<Prescription> Prescriptions    { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems    { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Specialty> Specialties  { get; set; }
        public DbSet<Visit> Visits  { get; set; }
        public DbSet<VisitNote> VisitNotes { get; set; }
        public DbSet<ApplicationUserOTP> ApplicationUserOTPs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBcontext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
