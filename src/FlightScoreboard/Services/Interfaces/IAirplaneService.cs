using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirplaneService
{
	List<AirplaneModel> GetAllAirplanes();
	int CreateAirplane(AirplaneCreateModel airplane);
	bool DeleteAirplane(int id);
}