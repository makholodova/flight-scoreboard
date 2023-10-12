
namespace FlightScoreboard.Services.Models;

public class ScoreboardIndexModel
{
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string AirlineName { get; set; }
    public int AirlineId { get; set; }
    public string AirplaneModel { get; set; }
    public int AirplaneId { get; set; }
    public string FromCity { get; set; }
    public string ToCity { get; set; }
}