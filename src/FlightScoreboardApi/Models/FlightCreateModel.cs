using System;

namespace FlightScoreboardApi.Models;

public class FlightCreateModel
{
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }

	public DateTime? ActualDepartureTime { get; set; }
	public DateTime? ActualArrivalTime { get; set; }
	public DateTime? CheckInStartTime { get; set; }
	public DateTime? CheckInEndTime { get; set; }
	public DateTime? BoardingStartTime { get; set; }
	public DateTime? BoardingEndTime { get; set; }

	public int FromCityId { get; set; }
	public int ToCityId { get; set; }
	public int PilotId { get; set; }
	public int AirlineId { get; set; }
	public int AirlineAirplaneId { get; set; }

	public string NumberOfFlight { get; set; }
	public string ToGate { get; set; }
	public string ToTerminal { get; set; }
	public string FromGate { get; set; }
	public string FromTerminal { get; set; }
}