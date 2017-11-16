namespace CarsApplication.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<PartSupplier> Suppliers { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId);

            builder.Entity<Car>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Car)
                .HasForeignKey(s => s.CarId);

            builder.Entity<PartSupplier>()
                .HasMany(s => s.Parts)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            // Many-to-many
            builder.Entity<PartCars>()
                .HasKey(pc => new { pc.CarId, pc.PartId });

            builder.Entity<PartCars>()
                .HasOne(pc => pc.Car)
                .WithMany(c => c.PartCars)
                .HasForeignKey(pc => pc.CarId);

            builder.Entity<PartCars>()
                .HasOne(pc => pc.Part)
                .WithMany(c => c.PartCars)
                .HasForeignKey(pc => pc.PartId);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Logs)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
