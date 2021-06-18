using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class CalisanKariyerMapping : IEntityTypeConfiguration<CalisanKariyer>
    {
        public void Configure(EntityTypeBuilder<CalisanKariyer> builder)
        {
            builder.HasKey(a => a.CalisanKariyerId);
            builder.Property(a => a.CalisanKariyerId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.Property(a => a.CalismaSekli).HasMaxLength(100);

          

            builder.Property(a => a.Sube).HasMaxLength(150);

            builder.HasOne(a => a.Calisan).WithMany(a => a.CalisanKariyer).HasForeignKey(a => a.CalisanId);
        }
    }
}
