using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class ResmiTatilMapping : IEntityTypeConfiguration<ResmiTatil>
    {
        public void Configure(EntityTypeBuilder<ResmiTatil> builder)
        {
            builder.HasKey(a => a.ResmiTatilId);
            builder.Property(a => a.ResmiTatilId).ValueGeneratedOnAdd().UseIdentityColumn(1,1);


        }
    }
}
