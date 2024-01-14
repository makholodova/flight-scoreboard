using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class ScoreboardDepartureIndexIpModel
{
    public DateTime? DateTime { get; set; }
    public int? CityId { get; set; }
    public List<CityModel> Cities { get; set; }
    public List<ScoreboardDepartureIndexModel> Flights { get; set; }
}