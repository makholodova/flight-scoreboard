namespace FlightScoreboard.Services.Models;

public class ScoreboardArrivalIndexModel
{
    public string AirlineName { get; set; }
    public int AirlineId { get; set; }
    public string NumberOfFlight { get; set; }
    public string FromCity { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string AirplaneModel { get; set; }
    public int AirplaneId { get; set; }
    public string Terminal { get; set; }
    public string Gate { get; set; }
}