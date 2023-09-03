using FlightScoreboard.DateBase.Configs;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.DateBase;

public class FlightScoreboardContext : DbContext
{
	public FlightScoreboardContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Airline> Airlines { get; set; } 
	public DbSet<Airplane> Airplanes { get; set; } 
	public DbSet<AirlineAirplane> AirlineAirplanes { get; set; }
	public DbSet<City> Cities { get; set; }
	public DbSet<Flight> Flights { get; set; }
	public DbSet<Pilot> Pilots { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AirlineConfiguration());
		modelBuilder.ApplyConfiguration(new AirlineAirplaneConfiguration());
		modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
		modelBuilder.ApplyConfiguration(new CityConfiguration());
		modelBuilder.ApplyConfiguration(new FlightConfiguration());
		modelBuilder.ApplyConfiguration(new PilotConfiguration());
	}
}