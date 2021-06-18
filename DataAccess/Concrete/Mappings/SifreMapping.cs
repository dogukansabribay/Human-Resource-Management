using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class SifreMapping : IEntityTypeConfiguration<Sifre>
    {
        public void Configure(EntityTypeBuilder<Sifre> builder)
        {
            builder.HasKey(a => a.SifreId);
            builder.Property(a => a.SifreId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.HasOne<Calisan>(a => a.Calisan)
           .WithMany(a => a.Sifreler)
           .HasForeignKey(a=>a.CalisanId);
        }
    }
}
