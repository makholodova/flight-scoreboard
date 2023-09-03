using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineAirplaneService
{
	List<AirlineAirplaneIndexModel> GetAllAAirlineAirplanes(int airlineId);
	AirlineAirplaneModel GetAirplaneAirlineById(int id);
	int CreateAirplane(AirlineAirplaneCreateModel airplane);
	bool UpdateAirplane(AirlineAirplaneUpdateModel airplane);
	bool DeleteAirplane(int id);
}