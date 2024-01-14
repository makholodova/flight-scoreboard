using System;

namespace FlightScoreboardData.Services.Models;

public class FlightIndexFilterModel
{
	public DateTime? DepartureTime { get; set; }
	public DateTime? ArrivalTime { get; set; }
	public int? FromCityId { get; set; }
	public int? ToCityId { get; set; }
	public int? PilotId { get; set; }
	public int? AirlineId { get; set; }
	public int? AirplaneId { get; set; }
	public string? NumberOfFlight { get; set; }
}