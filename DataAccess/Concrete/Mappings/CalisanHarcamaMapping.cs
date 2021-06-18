using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class CalisanHarcamaMapping : IEntityTypeConfiguration<CalisanHarcama>
    {
        public void Configure(EntityTypeBuilder<CalisanHarcama> builder)
        {
            builder.HasKey(a => a.CalisanHarcamaID);
            builder.Property(a => a.CalisanHarcamaID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.HasOne(a => a.Calisan).WithMany(a => a.CalisanHarcama).HasForeignKey(a => a.CalisanId);



        }
    }
}
