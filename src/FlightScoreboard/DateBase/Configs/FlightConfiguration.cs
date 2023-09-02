﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboard.DateBase.Configs;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
	public void Configure(EntityTypeBuilder<Flight> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.Time).IsRequired();
		builder.HasOne(p => p.Airline).WithMany(p => p.Flights).HasForeignKey(p => p.AirlineId);
		builder.HasOne(p => p.AirplaneByAirline).WithMany(p => p.Flights).HasForeignKey(p => p.AirplaneByAirlineId).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(p => p.Pilot).WithMany(p => p.Flights).HasForeignKey(p => p.PilotId).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(p => p.FromCity).WithMany(p => p.FromFlights).HasForeignKey(p => p.FromCityId).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(p => p.ToCity).WithMany(p => p.ToFlights).HasForeignKey(p => p.ToCityId).OnDelete(DeleteBehavior.NoAction);
	}
}