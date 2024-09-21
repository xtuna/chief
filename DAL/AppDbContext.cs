using Microsoft.EntityFrameworkCore;

namespace chief.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Register the trigger
            modelBuilder.Entity<Application>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateApplicationStatus"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
