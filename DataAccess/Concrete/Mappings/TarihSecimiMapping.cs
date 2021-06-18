using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mappings
{
    public class TarihSecimiMapping : IEntityTypeConfiguration<TarihSecimi>
    {
        public void Configure(EntityTypeBuilder<TarihSecimi> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}
