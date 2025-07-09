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
    public DbSet<Valore> Valori { get; set; }

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
        .HasOne(v => v.Account)
        .WithMany(a => a.Valori)
        .HasForeignKey(v => v.AccountId);

    modelBuilder.Entity<Valore>()
        .Property(v => v.ValoreNumerico)
        .HasPrecision(18, 4);


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
    .HasOne(r => r.Account)
    .WithOne(a => a.Role)
    .HasForeignKey<Role>(r => r.AccountId) // Assicurati che AccountId sia la chiave esterna in Role
    .OnDelete(DeleteBehavior.Cascade);



}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.ConfigureWarnings(warnings =>
          warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
    }
  }
}
