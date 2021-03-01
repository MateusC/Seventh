using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Seventh.DGuard.Domain.Entities;

namespace Seventh.DGuard.Infra.Data.Sql
{
    public class DGuardContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Recycler> Recyclers { get; set; }

        public DGuardContext(DbContextOptions<DGuardContext> options) : base(options)
        {
        }

        public DGuardContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dguard");

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }

    public class DGuardContextFactory : IDesignTimeDbContextFactory<DGuardContext>
    {
        public DGuardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DGuardContext>();

            var connectionString = "Data Source=(localdb)\\localhost;initial catalog=teste;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);

            return new DGuardContext(optionsBuilder.Options);
        }
    }
}
