namespace FlightScoreboard.DateBase;

public class AirplaneByAirline
{
	public AirplaneByAirline(int id, int serialNumber, int airlineId, int airplaneId)
	{
		this.Id = id;
		this.SerialNumber = serialNumber;
		this.AirlineId = airlineId;
		this.AirplaneId = airplaneId;
	}

	public int Id { get; set; }
	public int SerialNumber { get; set; }
	
	public int AirlineId { get; set; }
	public virtual Airline Airline { get; set; }
	
	public int AirplaneId { get; set; }
	public virtual Airplane Airplane { get; set; }
	
	public virtual List<Flight> Flights { get; set; }
}