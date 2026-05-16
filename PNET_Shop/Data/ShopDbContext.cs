using Microsoft.EntityFrameworkCore;
using PNET_Shop.Models;

namespace PNET_Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Good> Goods => Set<Good>();
        public DbSet<Check> Checks => Set<Check>();
        public DbSet<Sale> Sales => Set<Sale>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
            modelBuilder.Entity<Good>().ToTable("Goods");
            modelBuilder.Entity<Check>().ToTable("Checks");
            modelBuilder.Entity<Sale>().ToTable("Sales");

            modelBuilder.Entity<Department>()
                .Property(d => d.DeptId)
                .HasColumnName("DEPT_ID")
                .HasColumnType("decimal(4,0)");

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasColumnName("NAME");

            modelBuilder.Entity<Department>()
                .Property(d => d.Info)
                .HasColumnName("INFO");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.SupplierId)
                .HasColumnName("SUPPLIER_ID");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Name)
                .HasColumnName("NAME");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Phone)
                .HasColumnName("PHONE");

            modelBuilder.Entity<Supplier>()
                .Property(s => s.Address)
                .HasColumnName("ADDRESS");

            modelBuilder.Entity<Good>()
                .Property(g => g.GoodId)
                .HasColumnName("GOOD_ID");

            modelBuilder.Entity<Good>()
                .Property(g => g.Name)
                .HasColumnName("NAME");

            modelBuilder.Entity<Good>()
                .Property(g => g.Price)
                .HasColumnName("PRICE");

            modelBuilder.Entity<Good>()
                .Property(g => g.Quantity)
                .HasColumnName("QUANTITY");

            modelBuilder.Entity<Good>()
                .Property(g => g.Producer)
                .HasColumnName("PRODUCER");

            modelBuilder.Entity<Good>()
                .Property(g => g.DeptId)
                .HasColumnName("DEPT_ID")
                .HasColumnType("decimal(4,0)");

            modelBuilder.Entity<Good>()
                .Property(g => g.SupplierId)
                .HasColumnName("SUPPLIER_ID");

            modelBuilder.Entity<Good>()
                .Property(g => g.Description)
                .HasColumnName("DESCRIPTION");

            modelBuilder.Entity<Check>()
                .Property(c => c.CheckNo)
                .HasColumnName("CHECK_NO");

            modelBuilder.Entity<Check>()
                .Property(c => c.CheckDate)
                .HasColumnName("CHECK_DATE");

            modelBuilder.Entity<Check>()
                .Property(c => c.TotalSum)
                .HasColumnName("TOTAL_SUM");

            modelBuilder.Entity<Check>()
                .Property(c => c.CashierName)
                .HasColumnName("CASHIER_NAME");

            modelBuilder.Entity<Sale>()
                .Property(s => s.SaleId)
                .HasColumnName("SALE_ID");

            modelBuilder.Entity<Sale>()
                .Property(s => s.CheckNo)
                .HasColumnName("CHECK_NO");

            modelBuilder.Entity<Sale>()
                .Property(s => s.GoodId)
                .HasColumnName("GOOD_ID");

            modelBuilder.Entity<Sale>()
                .Property(s => s.DateSale)
                .HasColumnName("DATE_SALE");

            modelBuilder.Entity<Sale>()
                .Property(s => s.Quantity)
                .HasColumnName("QUANTITY");

            modelBuilder.Entity<Good>()
                .HasOne(g => g.Department)
                .WithMany(d => d.Goods)
                .HasForeignKey(g => g.DeptId);

            modelBuilder.Entity<Good>()
                .HasOne(g => g.Supplier)
                .WithMany(s => s.Goods)
                .HasForeignKey(g => g.SupplierId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Check)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CheckNo);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Good)
                .WithMany(g => g.Sales)
                .HasForeignKey(s => s.GoodId);
        }
    }
}
