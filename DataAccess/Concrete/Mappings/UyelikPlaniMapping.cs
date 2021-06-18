using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class UyelikPlaniMapping : IEntityTypeConfiguration<UyelikPlani>
    {
        public void Configure(EntityTypeBuilder<UyelikPlani> builder)
        {
            builder.HasKey(a => a.UyelikPlaniID);
            builder.Property(a => a.UyelikPlaniID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.HasMany(a => a.SirketUyelikPlani).WithOne(a => a.UyelikPlani).HasForeignKey(a => a.UyelikPlaniID).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
