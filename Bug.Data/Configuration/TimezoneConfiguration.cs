using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bug.Data.Configuration
{
    class TimezoneConfiguration : IEntityTypeConfiguration<Timezone>
    {
        public void Configure(EntityTypeBuilder<Timezone> builder)
        {
            builder
                .ToTable("Timezone")
                .Property(b => b.GmtOffset)
                .IsRequired();
            builder
                .Property(b => b.CountryCode)
                .IsRequired();
            builder
                .Property(b => b.CountryName)
                .IsRequired();
        }
    }
}
