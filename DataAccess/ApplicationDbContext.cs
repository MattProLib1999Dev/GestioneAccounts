using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore.Diagnostics;
=======
>>>>>>> origin/main

namespace GestioneAccounts.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; } = default!;
<<<<<<< HEAD
        public DbSet<Valori> Valori { get; set; } = default!;
=======
>>>>>>> origin/main

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
<<<<<<< HEAD

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

        modelBuilder.Entity<Valori>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Valori)
            .WithOne(v => v.Account) 
            .HasForeignKey(v => v.Id) 
            .IsRequired(false);
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
      }



=======
>>>>>>> origin/main
    }
}
