using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GestioneAccounts.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Valori> Valori { get; set; } = default!;

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

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



    }
}
