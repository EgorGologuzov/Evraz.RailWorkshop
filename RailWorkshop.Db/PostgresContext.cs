using Microsoft.EntityFrameworkCore;
using RailWorkshop.Services.Entity;

namespace RailWorkshop.Db
{
    public class PostgresContext : DbContext
    {
        private string _connectionString;

        public DbSet<Defect> Defects { get; set; }
        public DbSet<RailProfile> RailProfiles { get; set; }
        public DbSet<SteelGrade> SteelGrades { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkshopSegment> WorkshopSegments { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<ProductDefect> ProductDefects { get; set; }
        public DbSet<SegmentAccount> SegmentAccounts { get; set; }

        public PostgresContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PostgresContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString is not null)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }

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