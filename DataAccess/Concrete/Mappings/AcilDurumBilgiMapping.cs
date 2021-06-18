using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class AcilDurumBilgiMapping : IEntityTypeConfiguration<AcilDurumBilgi>
    {
        public void Configure(EntityTypeBuilder<AcilDurumBilgi> builder)
        {

            builder.HasKey(a => a.AcilDurumBilgiId);
            builder.Property(a => a.AcilDurumBilgiId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.Property(a => a.AranacakKisiAd).HasMaxLength(60);

            builder.Property(a => a.AranacakKisiSoyad).HasMaxLength(60);

            builder.Property(a => a.TelefonNo).HasMaxLength(13);// +90 555 555 55 55 

            builder.Property(a => a.YakinlikDerecesi).HasMaxLength(60);

            builder.HasOne<Calisan>(a => a.Calisan)
                .WithOne(a => a.AcilDurumBilgi)
                .HasForeignKey<AcilDurumBilgi>(a => a.CalisanId);
        }
    }
}
