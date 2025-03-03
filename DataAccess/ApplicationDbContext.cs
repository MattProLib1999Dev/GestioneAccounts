using GestioneAccounts.BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GestioneAccounts.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Valore> Valori { get; set; } = default!;  // ✅ Match the class name
        public DbSet<Valore> Valore { get; set; } = default!;  // ✅ Match the class name

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Valore>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Valori)
                .WithOne(v => v.Account)  // ✅ Correct navigation property name
                .HasForeignKey(v => v.AccountId)
                .IsRequired(false);  // ✅ If optional, AccountId should be nullable
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
