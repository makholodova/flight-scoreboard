using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineService
{
	List<AirlineModel> GetAllAirlines();
	int CreateAirline(AirlineCreateModel airline);
	bool DeleteAirline(int id);
}