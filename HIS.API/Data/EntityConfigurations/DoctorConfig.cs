using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.FullName).IsRequired().HasMaxLength(200);
            builder.Property(d => d.EmployeeNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Phone).HasMaxLength(20);
            builder.Property(d => d.Email).HasMaxLength(200);

            builder.HasOne(d => d.Department)
                   .WithMany(dep => dep.Doctors)
                   .HasForeignKey(d => d.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(d => d.Appointments)
                   .WithOne(a => a.Doctor)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Visits)
                   .WithOne(v => v.Doctor)
                   .HasForeignKey(v => v.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
