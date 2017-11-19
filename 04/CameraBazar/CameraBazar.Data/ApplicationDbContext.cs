namespace CameraBazar.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
           
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Cameras)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
