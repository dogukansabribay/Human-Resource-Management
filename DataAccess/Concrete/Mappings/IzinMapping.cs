using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class IzinMapping : IEntityTypeConfiguration<Izin>
    {
        public void Configure(EntityTypeBuilder<Izin> builder)
        {
            builder.HasKey(a => a.IzinID);
            builder.Property(a => a.IzinID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);
            builder.Property(a => a.IzinBelgesiYolu).IsRequired(false);

        }
    }
}
