using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;
using WarrantyManagement.Entities;
using WarrantyRepository.Entities;

namespace WarrantyManagement.Repositories
{
    public class WarrantyManagementDbContext : IdentityDbContext<User>
    {
        public WarrantyManagementDbContext(DbContextOptions<WarrantyManagementDbContext> options) : base(options)
        {
        }

        //public DbSet<Category> Categories { get; set; }
        public DbSet<User> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<WarrantyDevice> WarrantyDevices { get; set; }
        public DbSet<WarrantyDeviceHistory> WarrantyDeviceHistories { get; set; }
        public DbSet<WarrantyHistory> WarrantyHistories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Warranties)
                .WithOne(w => w.Customer)
                .HasForeignKey(w => w.CustomerId);

            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.Devices)
            //    .WithOne(w => w.User)
            //    .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<WarrantyDevice>()
                .HasKey(wd => new { wd.WarrantyId, wd.DeviceId });

            modelBuilder.Entity<WarrantyDevice>()
                .HasOne(rp => rp.Warranty)
                .WithMany(rp => rp.WarrantyDevices)
                .HasForeignKey(rp => rp.WarrantyId);

            modelBuilder.Entity<WarrantyDevice>()
                .HasOne(rp => rp.Device)
                .WithMany(rp => rp.WarrantyDevices)
                .HasForeignKey(rp => rp.DeviceId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<Warranty>()
                .HasMany(c => c.WarrantyHistories)
                .WithOne(w => w.Warranty)
                .HasForeignKey(w => w.WarrantyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Devices)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
