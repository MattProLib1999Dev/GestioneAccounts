using GestioneAccounts.BE.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Namespace.GestioneAccounts.Configuration.Models.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GestioneAccounts.DataAccess
{
    // Assumo che Account erediti da IdentityUser<string> o IdentityUser
    public class ApplicationDbContext : IdentityDbContext<Account, IdentityRole, string>
    {
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Valore> Valori { get; set; } = default!;

        public DbSet<Role> Roles { get; set; } = default!;

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
      base.OnModelCreating(modelBuilder);

      // Configurazione relazione Valore <-> Account
      modelBuilder.Entity<Valore>()
         .HasOne(v => v.Account)
         .WithMany(a => a.Valori)
         .HasForeignKey(v => v.AccountId);

      modelBuilder.Entity<Account>()
      .HasMany(a => a.Roles)
      .WithMany(r => r.Accounts)
      .UsingEntity(j => j.ToTable("RolesAccounts"));

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    optionsBuilder.ConfigureWarnings(warnings =>
                        warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
                }
    }
}
