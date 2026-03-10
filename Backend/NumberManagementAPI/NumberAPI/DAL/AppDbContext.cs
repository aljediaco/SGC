using Microsoft.EntityFrameworkCore;
using NumberAPI.Models;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<CounterRecord> CounterRecords { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CounterRecord>(entity =>
		{
			entity.ToTable("CounterRecords");
			entity.HasKey(e => e.Id);
			entity.Property(e => e.NumberValue).IsRequired();
			entity.Property(e => e.SavedDate).HasColumnType("datetime");
		});
	}
}