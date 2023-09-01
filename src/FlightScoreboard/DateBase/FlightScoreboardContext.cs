using FlightScoreboard.DateBase.Configs;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.DateBase;

public class FlightScoreboardContext : DbContext
{
	public FlightScoreboardContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Airline> Airline { get; set; }
	public DbSet<Airplane> Airplane { get; set; }
	public DbSet<AirplaneByAirline> AirplaneByAirline { get; set; }
	public DbSet<City> City { get; set; }
	public DbSet<Flight> Flight { get; set; }
	public DbSet<Pilot> Pilot { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AirlineConfiguration());
		modelBuilder.ApplyConfiguration(new AirplaneByAirlineConfiguration());
		modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
		modelBuilder.ApplyConfiguration(new CityConfiguration());
		modelBuilder.ApplyConfiguration(new FlightConfiguration());
		modelBuilder.ApplyConfiguration(new PilotConfiguration());
	}
}