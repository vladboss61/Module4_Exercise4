using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntroConsoleEF.Configurations
{
    public class CompanyConfguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
               .ToTable("Company")
               .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("CompanyId");
        }
    }
}
