namespace FlightScoreboard.Services.Models;

public class ScoreboardDepartureIndexModel
{
    public string AirlineName { get; set; }
    public int AirlineId { get; set; }
    public string NumberOfFlight { get; set; }
    public string ToCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public string AirplaneModel { get; set; }
    public int AirplaneId { get; set; }
    public string Terminal { get; set; }
    public string Gate { get; set; }
}