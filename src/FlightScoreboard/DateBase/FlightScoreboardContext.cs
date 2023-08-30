using FlightScoreboard.DateBase.Configs;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.DateBase;

public class FlightScoreboardContext : DbContext
{
	public FlightScoreboardContext(DbContextOptions options) : base(options)
	{
	}

	
	
	public DbSet<Airplane> Airplane { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
	}
}