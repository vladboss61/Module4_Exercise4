using ConsoleEFCoreLazy.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCoreLazy.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User")
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
        }
    }
}
