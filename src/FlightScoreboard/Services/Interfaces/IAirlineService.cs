using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineService
{
	List<AirlineModel> GetAllAirlines();
	List<AirlineShortInfoModel> GetAvailableAirlines();
	int CreateAirline(AirlineCreateModel airline);
	bool DeleteAirline(int id);
}