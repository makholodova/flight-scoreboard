using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Models;

public class ScoreboardIndexIpModel
{
    public DateTime? DateTime { get; set; }
    public int? CityId { get; set; }
    public List<CityModel> Cities { get; set; }
    public List<ScoreboardIndexModel> Flights { get; set; }
}