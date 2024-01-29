using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class WarrantyManagementDbContext : IdentityDbContext<User>
    {
        public WarrantyManagementDbContext(DbContextOptions<WarrantyManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Devices)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Warranties)
                .WithOne(w => w.Customer)
                .HasForeignKey(w => w.CustomerId);

            modelBuilder.Entity<Device>()
                .HasMany(d => d.Warranties)
                .WithOne(w => w.Device)
                .HasForeignKey(w => w.DeviceId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new {rp.RoleId, rp.PermissionId});

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }

    //public class YourDbContextFactory : IDesignTimeDbContextFactory<WarrantyManagementDbContext>
    //{
    //    public WarrantyManagementDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<WarrantyManagementDbContext>();
    //        optionsBuilder.UseSqlServer("DefaultConnection");

    //        return new WarrantyManagementDbContext(optionsBuilder.Options);
    //    }
    //}
}
