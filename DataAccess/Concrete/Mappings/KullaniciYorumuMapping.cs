using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class KullaniciYorumuMapping : IEntityTypeConfiguration<KullaniciYorumu>
    {
        public void Configure(EntityTypeBuilder<KullaniciYorumu> builder)
        {
            builder.HasKey(a => a.KullaniciYorumuId);
            builder.Property(a => a.KullaniciYorumuId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.HasOne<Calisan>(a => a.Calisan).WithMany(a => a.KullaniciYorumlari).HasForeignKey(a => a.CalisanId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
