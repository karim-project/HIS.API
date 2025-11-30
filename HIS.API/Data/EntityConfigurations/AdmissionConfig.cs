using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class AdmissionConfig : IEntityTypeConfiguration<Admission>
    {
        public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.ToTable("Admissions");
            builder.HasKey(a => a.Id);


            builder.HasOne(a => a.Patient)
            .WithMany(p => p.Admissions)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);


          
        }
    }
}
