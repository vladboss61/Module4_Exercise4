using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVersion> ProductVersions { get; set; }

    public virtual DbSet<SupplyHistory> SupplyHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ConsoleApplication;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Car");

            entity.Property(e => e.Color).HasMaxLength(150);
            entity.Property(e => e.NameCar)
                .HasMaxLength(50)
                .HasColumnName("Name_Car");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.HasIndex(e => e.ProductId, "IX_Company_ProductId");

            entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.Companies).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<ProductVersion>(entity =>
        {
            entity.ToTable("ProductVersion");

            entity.HasIndex(e => e.ProductId, "IX_ProductVersion_ProductId").IsUnique();

            entity.HasOne(d => d.Product).WithOne(p => p.ProductVersion).HasForeignKey<ProductVersion>(d => d.ProductId);
        });

        modelBuilder.Entity<SupplyHistory>(entity =>
        {
            entity.HasIndex(e => e.CompanyId, "IX_SupplyHistories_CompanyId");

            entity.HasIndex(e => e.ProductId, "IX_SupplyHistories_ProductId");

            entity.HasOne(d => d.Company).WithMany(p => p.SupplyHistories).HasForeignKey(d => d.CompanyId);

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyHistories).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.CompanyId, "IX_User_CompanyId");

            entity.HasOne(d => d.Company).WithMany(p => p.Users).HasForeignKey(d => d.CompanyId);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.UserId, "IX_UserProfile_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile).HasForeignKey<UserProfile>(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
