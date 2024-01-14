using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightScoreboardData.DateBase.Configs;

public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
{
	public void Configure(EntityTypeBuilder<Airplane> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
		builder.Property(p => p.Model).IsRequired().HasMaxLength(50);
	}
}