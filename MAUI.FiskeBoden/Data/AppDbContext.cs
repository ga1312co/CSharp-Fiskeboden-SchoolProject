using System;
using System.Collections.Generic;
using Informatics.FiskeBoden.Models;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskeBoden.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<PickupPoint> PickupPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierPickupPoint> SupplierPickupPoints { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__Batch__5D55CE38D6F66258");

            entity.ToTable("Batch");

            entity.HasIndex(e => e.BatchNo, "UQ__Batch__5D56EB96CC85A597").IsUnique();

            entity.Property(e => e.BatchId).HasColumnName("BatchID");
            entity.Property(e => e.BatchNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BatchOrigin)
                .HasMaxLength(255)
                .HasDefaultValue("Unknown");
            entity.Property(e => e.BatchPrice)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BatchQuantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BatchShelfLife).HasDefaultValue(15);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductionDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Product).WithMany(p => p.Batches)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Batch_Product");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Batches)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Batch_Supplier");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B3CDB1A16");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryNo, "UQ__Category__19093271ED4F0680").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CategoryNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PickupPoint>(entity =>
        {
            entity.HasKey(e => e.PickupPointId).HasName("PK__PickupPo__195D7E80BB1DD14B");

            entity.ToTable("PickupPoint");

            entity.HasIndex(e => e.PickupPointNo, "UQ__PickupPo__195D064E50CACE49").IsUnique();

            entity.Property(e => e.PickupPointId).HasColumnName("PickupPointID");
            entity.Property(e => e.PickupPointAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PickupPointName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PickupPointNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED0AE0F537");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductNo, "UQ__Product__B40D3A37BE2DB833").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694A2AC8B8D");

            entity.ToTable("Supplier");

            entity.HasIndex(e => e.SupplierSwishNumber, "UQ__Supplier__274A44D82A3F0798").IsUnique();

            entity.HasIndex(e => e.SupplierNo, "UQ__Supplier__4BE699815CC87513").IsUnique();

            entity.HasIndex(e => e.SupplierEmail, "UQ__Supplier__50A85C13C289D831").IsUnique();

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupplierSwishNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupplierPickupPoint>(entity =>
        {
            entity.HasKey(e => e.SupplierPickupPointId).HasName("PK__Supplier__E9A2AAADC490D050");

            entity.ToTable("SupplierPickupPoint");

            entity.Property(e => e.SupplierPickupPointId).HasColumnName("SupplierPickupPointID");
            entity.Property(e => e.PickupPointId).HasColumnName("PickupPointID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.PickupPoint).WithMany(p => p.SupplierPickupPoints)
                .HasForeignKey(d => d.PickupPointId)
                .HasConstraintName("FK_SupplierPickupPoint_PickupPoint");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierPickupPoints)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_SupplierPickupPoint_Supplier");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
