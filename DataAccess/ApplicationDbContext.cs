using GestioneAccounts.BE.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Namespace.GestioneAccounts.Configuration.Models.DTOs;

namespace GestioneAccounts.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Valore> Valori { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Account, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Necessario per Identity

            modelBuilder.Entity<Valore>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Valori)
                .WithOne(v => v.Account)
                .IsRequired(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
        }
    }
}
