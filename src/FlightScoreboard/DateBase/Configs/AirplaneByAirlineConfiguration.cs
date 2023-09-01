using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboard.DateBase.Configs;

public class AirplaneByAirlineConfiguration : IEntityTypeConfiguration<AirplaneByAirline>
{
	public void Configure(EntityTypeBuilder<AirplaneByAirline> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.SerialNumber).IsRequired().HasMaxLength(50);

		builder.HasOne(p => p.Airplane).WithMany(p => p.AirplaneByAirlines).HasForeignKey(p => p.AirplaneId);
		builder.HasOne(p => p.Airline).WithMany(p => p.AirplaneByAirlines).HasForeignKey(p => p.AirlineId);
	}
}