using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class EgitimBilgiMapping : IEntityTypeConfiguration<EgitimBilgi>
    {
        public void Configure(EntityTypeBuilder<EgitimBilgi> builder)
        {
            builder.HasKey(a => a.EgitimBilgiId);
            builder.Property(a => a.EgitimBilgiId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.HasOne(a => a.Calisan).WithMany(a => a.EgitimBilgileri).HasForeignKey(a => a.CalisanId);
        }
    }
}
