using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class WarrantyManagementDbContext : DbContext
    {
        public WarrantyManagementDbContext(DbContextOptions<WarrantyManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Warranty> Warranties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Devices)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Warranties)
                .WithOne(w => w.Customer)
                .HasForeignKey(w => w.CustomerId);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.Warranties)
                .WithOne(w => w.Device)
                .HasForeignKey(w => w.DeviceId);
        }
    }
}
