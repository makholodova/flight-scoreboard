using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboardData.DateBase.Configs;

public class AirlineAirplaneConfiguration : IEntityTypeConfiguration<AirlineAirplane>
{
	public void Configure(EntityTypeBuilder<AirlineAirplane> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.SerialNumber).IsRequired().HasMaxLength(50);

		builder.HasOne(p => p.Airplane).WithMany(p => p.AirlineAirplanes).HasForeignKey(p => p.AirplaneId);
		builder.HasOne(p => p.Airline).WithMany(p => p.AirlineAirplanes).HasForeignKey(p => p.AirlineId);
	}
}