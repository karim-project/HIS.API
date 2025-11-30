using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class BedConfig : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            builder.ToTable("Beds");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BedNumber).IsRequired().HasMaxLength(50);


            builder.HasOne(b => b.Room)
            .WithMany(r => r.Beds)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(b => b.CurrentAdmission)
            .WithMany()
            .HasForeignKey(b => b.CurrentAdmissionId)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
