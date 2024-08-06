using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>()
                .Property(i => i.Value)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Expense>()
                .Property(e => e.Value)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Goal>()
                .Property(g => g.GoalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Goal>()
                .Property(g => g.SpentAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Goal>()
                .Property(g => g.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Goal>()
                .Property(g => g.Category)
                .HasConversion<int>();
        }
    }
}
