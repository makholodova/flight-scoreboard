using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboardData.DateBase.Configs;

public class PilotConfiguration : IEntityTypeConfiguration<Pilot>
{
	public void Configure(EntityTypeBuilder<Pilot> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
		builder.Property(p => p.SurName).IsRequired().HasMaxLength(30);
		builder.Property(p => p.Age).IsRequired().HasMaxLength(4);
		builder.HasOne(p => p.Airline).WithMany(p => p.Pilots).HasForeignKey(p => p.AirlineId);
	}
}