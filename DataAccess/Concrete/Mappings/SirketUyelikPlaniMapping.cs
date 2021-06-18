using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class SirketUyelikPlaniMapping : IEntityTypeConfiguration<SirketUyelikPlani>
    {
        public void Configure(EntityTypeBuilder<SirketUyelikPlani> builder)
        {

            builder.HasKey(a=>a.SirketUyelikPlaniID);
            builder.Property(a => a.SirketUyelikPlaniID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

            builder.HasOne<Sirket>(a => a.Sirket)
                .WithOne(a => a.SirketUyelikPlani)
                .HasForeignKey<SirketUyelikPlani>(a => a.SirketID);
        }
    }
}
