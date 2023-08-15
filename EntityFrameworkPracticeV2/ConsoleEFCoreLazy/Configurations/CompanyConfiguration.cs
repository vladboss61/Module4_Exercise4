using ConsoleEFCoreLazy.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEFCoreLazy.Configurations
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
        }
    }
}
