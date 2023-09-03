using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlightScoreboard.DateBase;

public class Flight
{
	
	public int Id { get; set; }
	public DateTime Time { get; set; }
	
	public int FromCityId { get; set; }
	public virtual City FromCity { get; set; }
	
	public int ToCityId { get; set; }
	public virtual City ToCity { get; set; }
	
	public int PilotId { get; set; }
	public virtual Pilot Pilot { get; set; }
	
	public int AirlineId { get; set; }
	public virtual Airline Airline { get; set; }
	
	public int AirlineAirplaneId { get; set; }
	public virtual AirlineAirplane AirlineAirplane { get; set; }
}