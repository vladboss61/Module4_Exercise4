﻿using ConsoleEFCoreLazy.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCoreLazy.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Product")
                .HasKey(p => p.Id);
            
            builder
                .Property(p => p.Id)
                .HasColumnName("ProductId")
                .ValueGeneratedNever();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
