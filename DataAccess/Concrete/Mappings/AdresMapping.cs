using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class AdresMapping : IEntityTypeConfiguration<Adres>
    {
        public void Configure(EntityTypeBuilder<Adres> builder)
        {
            builder.HasKey(a => a.AdresId);
            builder.Property(a => a.AdresId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.HasOne<Calisan>(a => a.Calisan).WithOne(a => a.Adres).HasForeignKey<Adres>(a => a.CalisanID);

        }
            
    }
}
