using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class SirketMapping : IEntityTypeConfiguration<Sirket>
    {
        public void Configure(EntityTypeBuilder<Sirket> builder)
        {
            builder.HasKey(a => a.SirketId);
            builder.Property(a => a.SirketId).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.HasMany(a => a.Calisanlar).WithOne(a => a.Sirket).HasForeignKey(a => a.SirketId).OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}
