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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=192.168.0.173,34300;Database=BekoIntegration;User ID=sa;Password=Dataadmin2010;integrated security=false;TrustServerCertificate=True;Connection Timeout=3600")
                         .EnableSensitiveDataLogging()
                         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
