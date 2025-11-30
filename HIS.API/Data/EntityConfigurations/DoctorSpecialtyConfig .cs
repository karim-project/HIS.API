using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class DoctorSpecialtyConfig : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.ToTable("DoctorSpecialties");

            // Composite Key
            builder.HasKey(ds => new { ds.DoctorId, ds.SpecialtyId });

            // Relationships
            builder.HasOne(ds => ds.Doctor)
                   .WithMany(d => d.DoctorSpecialties)
                   .HasForeignKey(ds => ds.DoctorId);

            builder.HasOne(ds => ds.Specialty)
                   .WithMany(s => s.DoctorSpecialties)
                   .HasForeignKey(ds => ds.SpecialtyId);

        }
    }
}
