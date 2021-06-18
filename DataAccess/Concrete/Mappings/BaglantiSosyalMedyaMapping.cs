using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class BaglantiSosyalMedyaMapping : IEntityTypeConfiguration<BaglantiSosyalMedya>
    {
        public void Configure(EntityTypeBuilder<BaglantiSosyalMedya> builder)
        {
            builder.HasKey(a => a.BaglantiSosyalMedyaID);
            builder.Property(a => a.BaglantiSosyalMedyaID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.Property(a => a.HesapTuru).HasMaxLength(150);

            builder.Property(a => a.BaglantiAdresi).HasMaxLength(150);
            builder.HasOne<Calisan>(a => a.Calisan)
                .WithOne(a => a.BaglantiSosyalMedya)
                .HasForeignKey<BaglantiSosyalMedya>(a => a.CalisanID);
        }
    }
}
