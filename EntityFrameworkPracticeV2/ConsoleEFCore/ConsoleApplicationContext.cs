using ConsoleEFCore.Configurations;
using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ConsoleEFCore
{
    public sealed class ConsoleApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<SupplyHistory> SupplyHistories { get; set; }

        public DbSet<Car> Cars { get; set; }

        public ConsoleApplicationContext(DbContextOptions<ConsoleApplicationContext> options) : base(options)
        {
        }

        #region
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseMemoryCache();
        //    optionsBuilder.UseSqlServer("Connection");
        //}
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
