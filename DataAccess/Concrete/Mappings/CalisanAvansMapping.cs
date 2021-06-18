using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    class CalisanAvansMapping : IEntityTypeConfiguration<CalisanAvans>
    {
        public void Configure(EntityTypeBuilder<CalisanAvans> builder)
        {
            builder.HasKey(x => x.CalisanAvansID);
            builder.Property(a => a.CalisanAvansID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(a => a.AvansMiktarı).HasColumnType("decimal");
            builder.HasOne(x => x.Calisan).WithMany(x => x.CalisanAvans).HasForeignKey(x => x.CalisanID);
        }
    }
}
