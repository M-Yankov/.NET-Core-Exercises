namespace CarsApplication
{
    using Data;
    using Data.Models;
    using CarsApplication.Loggers.ApplicationDbLogger;
    using Services.Contracts;
    using Services.Implementations;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(setup =>
                {
                    setup.Password.RequireDigit = false;
                    setup.Password.RequireUppercase = false;
                    setup.Password.RequireNonAlphanumeric = false;
                    setup.Password.RequireLowercase = false;
                    setup.Password.RequiredLength = 3;
                    setup.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            // services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICustomerService, CustomerService>()
                    .AddTransient<ICarService, CarService>()
                    .AddTransient<ISuppliersService, SuppliersService>()
                    .AddTransient<ISalesService, SalesService>()
                    .AddTransient<IPartService, PartService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>();
            
            loggerFactory.AddConsole()
                         .AddDebug()
                         .AddDatabaseLogger(dbContext);

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
