namespace FlightScoreboardData.DateBase;

public class Flight
{
	public int Id { get; set; }

	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }

	public DateTime? ActualDepartureTime { get; set; }
	public DateTime? ActualArrivalTime { get; set; }
	public DateTime? CheckInStartTime { get; set; }
	public DateTime? CheckInEndTime { get; set; }
	public DateTime? BoardingStartTime { get; set; }
	public DateTime? BoardingEndTime { get; set; }

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


	public string NumberOfFlight { get; set; }
	public string ToGate { get; set; }
	public string FromGate { get; set; }
	public string ToTerminal { get; set; }
	public string FromTerminal { get; set; }
}