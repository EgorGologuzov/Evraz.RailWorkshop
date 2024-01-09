using Microsoft.EntityFrameworkCore;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.Db
{
    public class PostgresContext : DbContext
    {
        public DbSet<Defect> Defects { get; set; }
        public DbSet<RailProfile> RailProfiles { get; set; }
        public DbSet<SteelGrade> SteelGrades { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkshopSegment> WorkshopSegments { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<ProductDefect> ProductDefects { get; set; }
        public DbSet<SegmentAccount> SegmentAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Options.ConnectionString);

            // Добвалено, чтобы устранить ошибку вставки даты времени
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SegmentAccount>()
                .HasOne(a => a.Segment)
                .WithOne()
                .HasForeignKey<SegmentAccount>(a => a.SegmentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}