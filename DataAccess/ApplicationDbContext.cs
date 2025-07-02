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
    public new DbSet<Role> Roles { get; set; } = default!;

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

    modelBuilder.Entity<Valore>()
        .Property(v => v.Id)
        .ValueGeneratedOnAdd();

    modelBuilder.Entity<Account>()
        .HasMany(a => a.Valori)
        .WithOne(v => v.Account)
        .IsRequired(false);

    modelBuilder.Entity<Role>()
        .HasKey(r => r.Id)
        .HasName("PK_Roles");

    modelBuilder.Entity<Role>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd(); // indica che il DB genera il valore
    });

// Se Role ha una proprietà di navigazione ICollection<Account> Accounts
// e Account ha una proprietà di navigazione Role Role (relazione 1 a molti)

modelBuilder.Entity<Role>()
    .HasMany(r => r.Accounts)
    .WithOne(a => a.Role)
    .HasForeignKey(a => a.RoleId)   // Usa RoleId come FK, non Id!
    .OnDelete(DeleteBehavior.Cascade);



}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.ConfigureWarnings(warnings =>
          warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
    }
  }
}
