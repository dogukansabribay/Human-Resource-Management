using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class CalisanMapping : IEntityTypeConfiguration<Calisan>
    {
        public void Configure(EntityTypeBuilder<Calisan> builder)
        {
            builder.HasKey(a => a.CalisanId);
            builder.Property(a => a.CalisanId).ValueGeneratedOnAdd().UseIdentityColumn(1,1);

            builder.Property(a => a.Adi).IsRequired().HasMaxLength(60);

            builder.Property(a => a.Soyadi).IsRequired().HasMaxLength(60);

            builder.Property(a => a.MailIs).IsRequired().HasMaxLength(150);

            builder.Property(a => a.MailKisisel).IsRequired().HasMaxLength(150);

            builder.Property(a => a.IseGirisTarihi).IsRequired();

            builder.Property(a => a.ErisimTuru).IsRequired();

            builder.Property(a => a.SozlesmeTuru).IsRequired();

            builder.HasMany<Izin>(a => a.Izinler)
                .WithOne(a => a.Calisan)
                .HasForeignKey(a => a.CalisanId);
            
            builder.HasMany<Sifre>(a => a.Sifreler)
                .WithOne(a => a.Calisan)
                .HasForeignKey(a => a.CalisanId);

        }
    }
}   
