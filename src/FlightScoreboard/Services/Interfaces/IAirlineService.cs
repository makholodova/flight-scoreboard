using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineService
{
	List<AirlineModel> GetAllAirline();
	int CreateAirline(AirlineCreateModel airline);
	bool DeleteAirline(int id);
}