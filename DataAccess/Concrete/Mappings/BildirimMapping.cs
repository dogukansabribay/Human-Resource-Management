using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class BildirimMapping : IEntityTypeConfiguration<Bildirim>
    {
        public void Configure(EntityTypeBuilder<Bildirim> builder)
        {
            builder.HasKey(a => a.BildirimId);
            builder.Property(a => a.BildirimId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.HasOne<Calisan>(a => a.Calisan)
                .WithMany(a => a.Bildirimler)
                .HasForeignKey(a => a.CalisanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
