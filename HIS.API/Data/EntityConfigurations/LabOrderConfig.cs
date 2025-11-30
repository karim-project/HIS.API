using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class LabOrderConfig : IEntityTypeConfiguration<LabOrder>
    {
        public void Configure(EntityTypeBuilder<LabOrder> builder)
        {
            builder.ToTable("LabOrders");
            builder.HasKey(l => l.Id);


            builder.HasOne(l => l.Visit)
            .WithMany(v => v.LabOrders)
            .HasForeignKey(l => l.VisitId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(l => l.LabOrderItems)
            .WithOne(li => li.LabOrder)
            .HasForeignKey(li => li.LabOrderId)
            .OnDelete(DeleteBehavior.Cascade);


         
        }
    }
}
