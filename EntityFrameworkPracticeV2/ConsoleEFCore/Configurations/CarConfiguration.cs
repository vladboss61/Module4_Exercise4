using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEFCore.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
               .ToTable("Car")
               .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName("CarId")
                .ValueGeneratedNever();

            builder
                .Property(c => c.Name)
                .HasColumnName("Name_Car")
                .HasMaxLength(50);

            builder
                .Property(c => c.Color)
                .HasColumnName("Color")
                .HasMaxLength(150);

            builder.HasData(new []
            {
                new Car
                {
                    Id = 1,
                    Name = "Audi",
                    Size = 250
                },
                new Car
                {
                    Id = 2,
                    Name = "Ford",
                    Size = 350
                }
            });
        }
    }
}
