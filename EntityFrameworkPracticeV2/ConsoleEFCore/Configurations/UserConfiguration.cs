using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ConsoleEFCore.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User")
                .HasKey(p => p.UserId);

            builder
                .Property(p => p.UserId)
                .ValueGeneratedNever();

            builder.HasData(new User
            {
                UserId = 77,
                CompanyId = 99,
                FirstName = "FirstName10",
                LastName = "FirstName10",
                HiredDate = DateTime.UtcNow,
            });
        }
    }
}
