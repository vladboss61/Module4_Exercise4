using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEFCore.Configurations
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
                .ValueGeneratedNever();

            //builder.Ignore(x => x.About);

            builder.HasData(new UserProfile
            {
                Id = 88,
                About = "Some additional info",
                ImageUrl = "123",
                UserId = 77
            });
        }
    }
}
