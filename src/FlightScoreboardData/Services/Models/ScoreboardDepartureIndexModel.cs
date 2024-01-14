using System;

namespace FlightScoreboardData.Services.Models;

public class ScoreboardDepartureIndexModel : IStatusCalculation
{
	public string AirlineName { get; set; }
	public int AirlineId { get; set; }
	public string NumberOfFlight { get; set; }
	public string ToCity { get; set; }
	public string AirplaneModel { get; set; }
	public int AirplaneId { get; set; }
	public string Terminal { get; set; }
	public string Gate { get; set; }
	public string StatusMessage { get; set; }
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
	public DateTime? ActualDepartureTime { get; set; }
	public DateTime? ActualArrivalTime { get; set; }
	public DateTime? CheckInStartTime { get; set; }
	public DateTime? CheckInEndTime { get; set; }
	public DateTime? BoardingStartTime { get; set; }
	public DateTime? BoardingEndTime { get; set; }
}