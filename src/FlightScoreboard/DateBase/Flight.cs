using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlightScoreboard.DateBase;

public class Flight
{
	public Flight(int id, DateTime time, int fromCityId, int toCityId, int pilotId, int airlineId, int airplaneByAirlineId)
	{
		this.Id = id;
		this.Time = time;
		this.FromCityId = fromCityId;
		this.ToCityId = toCityId;
		this.PilotId = pilotId;
		this.AirlineId = airlineId;
		this.AirplaneByAirlineId = airplaneByAirlineId;
	}

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
	
	public int AirplaneByAirlineId { get; set; }
	public virtual AirplaneByAirline AirplaneByAirline { get; set; }
}