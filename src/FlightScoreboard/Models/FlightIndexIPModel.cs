using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class FlightIndexIpModel
{
    public FlightIndexFilterModel Flight { get; set; }
    public List<FlightIndexModel> Flights { get; set; }
    public List<AirlineAirplaneShortModel> Airplanes { get; set; }
    public List<PilotIndexModel> Pilots { get; set; }
    public List<CityModel> Cities { get; set; }
    public List<AirlineShortInfoModel> Airlines { get; set; }
}