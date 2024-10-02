using Microsoft.EntityFrameworkCore;

namespace chief.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationFile> ApplicationFiles { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Notify> Notifs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateApplicationStatus"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
