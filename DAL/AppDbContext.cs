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
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Notify> Notifs { get; set; }
        public DbSet<Evaluator> Evaluators { get; set; }
        public DbSet<EvaluatorDocument> EvaluatorDocuments { get; set; } // Dummy table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checklist>()
                .HasMany(c => c.ChecklistItems)
                .WithOne(c => c.Checklist)
                .HasForeignKey(c => c.ChecklistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}