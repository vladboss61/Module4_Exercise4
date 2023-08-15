using ConsoleEFCoreLazy.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCoreLazy.Configurations
{
    public sealed class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder
                .ToTable("UserProfile")
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("UserProfileId")
                .ValueGeneratedNever();
        }
    }
}
