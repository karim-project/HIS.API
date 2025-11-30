using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class MedicationConfig: IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(200);


            builder.HasMany(m => m.Stocks)
            .WithOne(s => s.Medication)
            .HasForeignKey(s => s.MedicationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
