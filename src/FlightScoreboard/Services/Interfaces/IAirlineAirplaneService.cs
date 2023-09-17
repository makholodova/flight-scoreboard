using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineAirplaneService
{
	List<AirlineAirplaneShortModel> GetAllAirlineAirplanes(int airlineId);
	List<AirlineAirplaneShortModel> GetAllAirlineAirplanes();
	AirlineAirplaneModel GetAirplaneAirlineById(int id);
	int CreateAirplane(AirlineAirplaneCreateModel airplane);
	bool UpdateAirplane(AirlineAirplaneUpdateModel airplane);
	bool DeleteAirplane(int id);
}