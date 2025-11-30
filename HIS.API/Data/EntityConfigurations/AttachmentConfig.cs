using HIS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.API.Data.EntityConfigurations
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FileName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.BlobPath).IsRequired();


            builder.HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(a => a.Visit)
            .WithMany(v => v.Attachments)
            .HasForeignKey(a => a.VisitId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
