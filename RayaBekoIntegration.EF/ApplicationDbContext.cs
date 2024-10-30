using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using RayaBekoIntegration.WebAPI;

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
    public virtual DbSet<VWbeko> VWbekos { get; set; }
    public virtual DbSet<PendingOrderStatus> PendingOrderStatuses { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=192.168.0.173,34300;Database=BekoIntegration;User ID=sa;Password=Dataadmin2010;integrated security=false;TrustServerCertificate=True;Connection Timeout=3600")
                         .EnableSensitiveDataLogging()
                         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PendingOrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PendingO__3214EC07EE956BD7");

            entity.ToTable("PendingOrderStatus");

            entity.Property(e => e.BekoOrderNumber)
                .HasMaxLength(256)
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
                .HasMaxLength(256)
                .IsUnicode(false);
        });
        modelBuilder.Entity<VWbeko>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vWBeko");

            entity.Property(e => e.AdditionalImage)
                .HasMaxLength(4000)
                .HasColumnName("Additional_Image");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(38, 8)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CatName)
                .HasMaxLength(254)
                .HasColumnName("Cat Name");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(254)
                .HasColumnName("Category_Name");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("COLOR");
            entity.Property(e => e.ColorAr).HasColumnName("Color_Ar");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDDATETIME");
            entity.Property(e => e.DepName)
                .HasMaxLength(254)
                .HasColumnName("Dep Name");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Inventlocationid)
                .HasMaxLength(10)
                .HasColumnName("INVENTLOCATIONID");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(100)
                .HasColumnName("Item_code");
            entity.Property(e => e.ItemTax)
                .HasMaxLength(10)
                .HasColumnName("ITEM_TAX");
            entity.Property(e => e.ItemType)
                .HasMaxLength(10)
                .HasColumnName("ITEM_Type");
            entity.Property(e => e.Itemid)
                .HasMaxLength(20)
                .HasColumnName("ITEMID");
            entity.Property(e => e.MainImage)
                .HasMaxLength(4000)
                .HasColumnName("Main_Image");
            entity.Property(e => e.MenuItemAr).HasColumnName("MenuItem_Ar");
            entity.Property(e => e.MenuItemEn)
                .HasMaxLength(100)
                .HasColumnName("MenuItem_EN");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("MODEL");
            entity.Property(e => e.ModelImageUrl)
                .HasMaxLength(4000)
                .HasColumnName("ModelImage_URL");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .HasColumnName("Model_Name");
            entity.Property(e => e.Modifieddatetime)
                .HasColumnType("datetime")
                .HasColumnName("MODIFIEDDATETIME");
            entity.Property(e => e.Namealias)
                .HasMaxLength(20)
                .HasColumnName("NAMEALIAS");
            entity.Property(e => e.Product).HasColumnName("PRODUCT");
            entity.Property(e => e.Qty)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("QTY");
            entity.Property(e => e.QtyAvailphysical)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("QTY_AVAILPHYSICAL");
            entity.Property(e => e.RBrand)
                .HasMaxLength(60)
                .HasColumnName("R_Brand");
            entity.Property(e => e.RBrickcode)
                .HasMaxLength(60)
                .HasColumnName("R_BRICKCODE");
            entity.Property(e => e.RColor).HasColumnName("R_COLOR");
            entity.Property(e => e.ROrainvitemid)
                .HasMaxLength(60)
                .HasColumnName("R_ORAINVITEMID");
            entity.Property(e => e.ReportMainCategoryAr).HasColumnName("Report_Main_category_AR");
            entity.Property(e => e.ReportMainCategoryEn).HasColumnName("Report_Main_category_En");
            entity.Property(e => e.ShortDescriptionAr)
                .HasMaxLength(4000)
                .HasColumnName("Short_Description_Ar");
            entity.Property(e => e.ShortDescriptionEn)
                .HasMaxLength(4000)
                .HasColumnName("Short_Description_En");
            entity.Property(e => e.Sku)
                .HasMaxLength(100)
                .HasColumnName("SKU");
            entity.Property(e => e.SkuBrand)
                .HasMaxLength(60)
                .HasColumnName("SKU Brand");
            entity.Property(e => e.SkuColor).HasColumnName("SKU Color");
            entity.Property(e => e.SkuImage)
                .HasMaxLength(4000)
                .HasColumnName("SKU_Image");
            entity.Property(e => e.SkuModel)
                .HasMaxLength(60)
                .HasColumnName("SKU Model");
            entity.Property(e => e.Skuname)
                .HasMaxLength(4000)
                .HasColumnName("SKUName");
            //entity.Property(e => e.SubsidyCat)
            //    .HasMaxLength(500)
            //    .HasColumnName("Subsidy_Cat");
        });
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
