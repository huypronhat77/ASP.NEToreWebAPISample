using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions option) : base (option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(o => {
                o.ToTable("orders");
                o.HasKey(o => o.OrderId);
                o.Property(o => o.OrderDate).HasDefaultValue(DateTime.Now);
                o.Property(o => o.ReceiverName).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<OrderDetail>(od => {
                od.ToTable("orderDetails");

                od.HasKey(k => new { k.OrderId, k.ProdId});

                od.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);

                od.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProdId);

            
            });

        }

        #region DbSets
        // Represent for Entity in application and create Table in DB
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        #endregion

    }
}
