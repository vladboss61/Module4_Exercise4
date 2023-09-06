using IntroConsoleEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;

namespace IntroConsoleEF.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User")
                .HasKey(p => p.Id);

            builder
                 .Property(p => p.Id);
            //.ValueGeneratedNever();
            
            builder
                .HasOne(e => e.Profile)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
