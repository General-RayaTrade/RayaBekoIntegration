using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RayaBekoIntegration.EF;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatusLog> OrderStatusLogs { get; set; }
    public virtual DbSet<VWcityDistrict> VWcityDistricts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=192.168.0.173,34300;Database=BekoIntegration;User ID=sa;Password=Dataadmin2010;integrated security=false;TrustServerCertificate=True;Connection Timeout=3600")
                         .EnableSensitiveDataLogging()
                         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VWcityDistrict>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vWCityDistrict");

            entity.Property(e => e.CNameAr)
                .HasMaxLength(255)
                .HasColumnName("C_NameAR");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DName)
                .HasMaxLength(255)
                .HasColumnName("D_Name");
            entity.Property(e => e.DNameAr)
                .HasMaxLength(255)
                .HasColumnName("D_NameAR");
            entity.Property(e => e.Id).HasColumnName("ID");
        });
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07BB4DB1DD");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_User_RefreshToken");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0769A34689");

            entity.Property(e => e.AccessToken).IsUnicode(false);
            entity.Property(e => e.AccessTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false);
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0743F90F04");

            entity.ToTable(tb => tb.HasTrigger("TR_Orders_UpdateModificationDate"));

            entity.HasIndex(e => e.BekoOrderNumber, "IX_Orders_BekoOrderNumber");

            entity.HasIndex(e => e.BekoOrderStatus, "IX_Orders_BekoOrderStatus");

            entity.HasIndex(e => e.RayaOrderNumber, "IX_Orders_RayaOrderNumber");

            entity.HasIndex(e => e.RayaOrderStatus, "IX_Orders_RayaOrderStatus");

            entity.Property(e => e.BekoOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.BekoOrderStatus)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModificationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RayaOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.RayaOrderStatus)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07B4F527C4");

            entity.HasIndex(e => e.BekoOrderNumber, "IX_OrderDetails_BekoOrderNumber");

            entity.HasIndex(e => e.RayaOrderNumber, "IX_OrderDetails_RayaOrderNumber");

            entity.Property(e => e.BekoOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.RayaOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderStatusLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC07310EC51F");

            entity.ToTable(tb => tb.HasTrigger("TR_OrderStatusLogs_UpdateModificationDate"));

            entity.HasIndex(e => e.BekoOrderNumber, "IX_OrderStatusLogs_BekoOrderNumber");

            entity.HasIndex(e => e.BekoOrderStatus, "IX_OrderStatusLogs_BekoOrderStatus");

            entity.HasIndex(e => e.RayaOrderNumber, "IX_OrderStatusLogs_RayaOrderNumber");

            entity.HasIndex(e => e.RayaOrderStatus, "IX_OrderStatusLogs_RayaOrderStatus");

            entity.Property(e => e.BekoOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.BekoOrderStatus)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModificationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RayaOrderNumber)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.RayaOrderStatus)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
