using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class PrescriptionItemConfig : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.ToTable("PrescriptionItems");
            builder.HasKey(pi => pi.Id);


            builder.HasOne(pi => pi.Medication)
            .WithMany(m => m.PrescriptionItems)
            .HasForeignKey(pi => pi.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
