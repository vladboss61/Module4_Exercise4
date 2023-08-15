using System;
using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEFCore.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
               .ToTable("Company")
               .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("CompanyId")
                .ValueGeneratedNever();

            builder.HasData(new Company
            {
                Id = 99,
                FoundationDate = DateTime.UtcNow,
                Name = "A-Level",
                Revenue = 1000
            }, new Company
            {
                Id = 199,
                FoundationDate = DateTime.UtcNow.AddDays(1),
                Name = "Spotify",
                Revenue = 2000,
                ProductId = 999
            });
        }
    }
}
