using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.walletType)
                .WithMany(wt => wt.wallets)
                .HasForeignKey(w => w.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasQueryFilter(x => x.Dlt == false);
            //in khat bala kole query haei ke toosh user boode ra ba in shart yki mikone
            //age ino nakhai bayad to query ino bezari .IgnoreQueryFilters()
            modelBuilder.Entity<Role>().HasQueryFilter(x => x.Dlt == false);

            modelBuilder.Entity<ProductGroup>().HasQueryFilter(x => x.Dlt == false);
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.Dlt == false);

            //in khataye zir baraye table haye order va orderDetail 
            //ba in dg error nemide hengame sakht jadval ha va relation
            //chon migoft ina ertebat daran baham va age az yki pak beshe bayad mn az on yki pak konam
            //ma goftim nemikhad khodemoon handel mikonim
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

        }
    }
}
