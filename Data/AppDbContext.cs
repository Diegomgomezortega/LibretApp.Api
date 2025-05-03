using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using libretapp.Data.Entities;

namespace libretapp.Data;

public partial class AppDbContext : DbContext
{
   

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Debt> Debts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseMySql("server=149.50.147.223;port=3306;database=c2811120_libreta;user=develop;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Name, "idx_customer_name");

            entity.HasIndex(e => e.Surname, "idx_customer_surname");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(100);
        });

        modelBuilder.Entity<Debt>(entity =>
        {
            entity.HasKey(e => e.DebtId).HasName("PRIMARY");

            entity.ToTable("Debt");

            entity.HasIndex(e => e.CustomerId, "idx_debt_customerid");

            entity.HasIndex(e => new { e.CustomerId, e.IsPaid }, "idx_debt_customerid_ispaid");

            entity.HasIndex(e => e.DueDate, "idx_debt_duedate");

            entity.HasIndex(e => e.IsPaid, "idx_debt_ispaid");

            entity.HasIndex(e => e.ProductId, "idx_debt_productid");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPaid).HasDefaultValueSql("'0'");
            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.RemainingAmount).HasPrecision(10, 2);
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);

            entity.HasOne(d => d.Customer).WithMany(p => p.Debts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Debt_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.Debts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Debt_ibfk_2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PRIMARY");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.DebtId, "idx_payment_debtid");

            entity.HasIndex(e => e.PaymentDate, "idx_payment_paymentdate");

            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Debt).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DebtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payment_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.IsActive, "idx_product_isactive");

            entity.HasIndex(e => e.Name, "idx_product_name");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<ProductPriceHistory>(entity =>
        {
            entity.HasKey(e => e.PriceHistoryId).HasName("PRIMARY");

            entity.ToTable("ProductPriceHistory");

            entity.HasIndex(e => e.ProductId, "idx_pricehistory_productid");

            entity.HasIndex(e => e.StartDate, "idx_pricehistory_startdate");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductPriceHistory_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
