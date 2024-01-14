using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboardData.DateBase.Configs;

public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
	public void Configure(EntityTypeBuilder<Airline> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
	}
}