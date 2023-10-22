using ElectronicsStore.BackendApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.BackendApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().Property(x => x.Id).IsUnicode(false);

            builder.Entity<User>().Property(x => x.Id).IsUnicode(false);
            builder.Entity<User>().Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);

            builder.Entity<ImportBill>().HasOne(x => x.User).WithMany(x => x.ImportBills).HasForeignKey(x => x.UserId);
            builder.Entity<ImportBill>().HasOne(x => x.Supplier).WithMany(x => x.ImportBills).HasForeignKey(x => x.SupplierId);
            builder.Entity<ImportBill>().Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);

            builder.Entity<ImportBillDetail>().HasOne(x => x.ImportBill).WithMany(x => x.ImportBillDetails).HasForeignKey(x => x.ImportBillId);
            builder.Entity<ImportBillDetail>().HasOne(x => x.Product).WithMany(x => x.ImportBillDetails).HasForeignKey(x => x.ProductId);
            builder.Entity<ImportBillDetail>().HasKey(x => new { x.ImportBillId, x.ProductId });

            builder.Entity<Image>().HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId);
            builder.Entity<Image>().Property(x => x.IsDefaul).HasDefaultValue(false);

            builder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            builder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId);
            builder.Entity<Product>().Property(x => x.PurchaseCount).HasDefaultValue(0);
            builder.Entity<Product>().Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);

            builder.Entity<SaleBillDetail>().HasOne(x => x.SaleBill).WithMany(x => x.SaleBillDetails).HasForeignKey(x => x.SaleBillId);
            builder.Entity<SaleBillDetail>().HasOne(x => x.Product).WithMany(x => x.SaleBillDetails).HasForeignKey(x => x.ProductId);
            builder.Entity<SaleBillDetail>().HasKey(x => new { x.SaleBillId, x.ProductId });

            builder.Entity<SaleBill>().HasOne(x => x.User).WithMany(x => x.SaleBills).HasForeignKey(x => x.UserId);
            builder.Entity<SaleBill>().HasOne(x => x.Status).WithMany(x => x.SaleBills).HasForeignKey(x => x.StatusId);
            builder.Entity<SaleBill>().Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);



            base.OnModelCreating(builder);
        }
        public DbSet<ImportBill> ImportBills { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ImportBillDetail> ImportBillDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SaleBillDetail> SaleBillDetails { get; set; }
        public DbSet<SaleBill> SaleBills { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
