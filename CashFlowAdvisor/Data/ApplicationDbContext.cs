using CashFlowAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace CashFlowAdvisor.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<IncomeSource> IncomeSources { get; set; }
		public DbSet<Expense> Expenses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<IncomeSource>().HasKey(i => i.Id);
			modelBuilder.Entity<IncomeSource>(entity => {
				entity.Property(e => e.Title).IsRequired();
				entity.Property(e => e.Price).IsRequired();
				entity.Property(e => e.Created_at).IsRequired();
			});

			modelBuilder.Entity<Expense>().HasKey(e => e.Id);
			modelBuilder.Entity<Expense>(entity => {
				entity.Property(e => e.Title).IsRequired();
				entity.Property(e => e.Price).IsRequired();
				entity.Property(e => e.Created_at).IsRequired();
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
