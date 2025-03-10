using System;
using System.Collections.Generic;
using apiStock.Models;
using Microsoft.EntityFrameworkCore;

namespace apiStock.DAL.Context;

public partial class dbContext : DbContext
{
    public dbContext()
    {
    }

    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__category__3214EC272DDE10AB");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3214EC273F310145");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Stock).HasColumnName("STOCK");
            entity.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__CATEGO__52593CB8");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__products__SUPPLI__534D60F1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3214EC27CCEDDB33");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__stockMov__3214EC2798FBA3BE");

            entity.ToTable("stockMovement");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.Cuantity).HasColumnName("CUANTITY");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("TYPE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.StockMovements)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__stockMove__PRODU__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.StockMovements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__stockMove__USER___5441852A");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3214EC275D9AB898");

            entity.ToTable("supplier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CELLPHONE");
            entity.Property(e => e.Contact)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CONTACT");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC27AAD7BB0C");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.HashPass)
                .IsUnicode(false)
                .HasColumnName("hashPass");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RolId).HasColumnName("ROL_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__users__ROL_ID__5629CD9C");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__UserSess__C9F49290F762AE49");

            entity.Property(e => e.SessionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1)
                .HasColumnName("isactive");
            entity.Property(e => e.ModifDate)
                .HasColumnType("datetime")
                .HasColumnName("MODIF_DATE");
            entity.Property(e => e.ModifUser)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MODIF_USER");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent).HasColumnType("text");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSessi__UserI__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
