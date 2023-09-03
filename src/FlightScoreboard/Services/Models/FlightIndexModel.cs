using FlightScoreboard.DateBase;

namespace FlightScoreboard.Services.Models;

public class FlightIndexModel
{
	public int Id { get; set; }
	public DateTime Time { get; set; }
	public City FromCity { get; set; }
	public City ToCity { get; set; }
	public string PilotName { get; set; }
	public string AirlineName { get; set; }
	public string AirplaneByAirlineModel { get; set; }
	public int AirplaneByAirlineSerialNumber { get; set; }
}