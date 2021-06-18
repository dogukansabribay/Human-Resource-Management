using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class BankaMapping : IEntityTypeConfiguration<Banka>
    {
        public void Configure(EntityTypeBuilder<Banka> builder)
        {
            builder.HasKey(a => a.BankaId);
            builder.Property(a => a.BankaId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.Property(a => a.BankaAdi).HasMaxLength(150);

            builder.Property(a => a.HesapNo).HasMaxLength(25);

            builder.Property(a => a.IBAN).HasMaxLength(25);

            builder.HasOne<Calisan>(a => a.Calisan).WithOne(a => a.Banka).HasForeignKey<Banka>(a => a.CalisanId);
        }
    }
}
