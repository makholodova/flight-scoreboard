using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboardData.DateBase.Configs;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
	public void Configure(EntityTypeBuilder<City> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
	}
}