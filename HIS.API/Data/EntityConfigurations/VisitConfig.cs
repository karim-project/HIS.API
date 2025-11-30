using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class VisitConfig : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visits");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.VisitType).IsRequired();


            builder.HasOne(v => v.Patient)
            .WithMany(p => p.Visits)
            .HasForeignKey(v => v.PatientId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(v => v.Doctor)
            .WithMany(d => d.Visits)
            .HasForeignKey(v => v.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(v => v.Prescriptions)
            .WithOne(p => p.Visit)
            .HasForeignKey(p => p.VisitId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(v => v.LabOrders)
            .WithOne(l => l.Visit)
            .HasForeignKey(l => l.VisitId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(v => v.Attachments)
            .WithOne(a => a.Visit)
            .HasForeignKey(a => a.VisitId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
