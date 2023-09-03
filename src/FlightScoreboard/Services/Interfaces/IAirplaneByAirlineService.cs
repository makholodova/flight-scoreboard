using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirplaneByAirlineService
{
	List<AirplaneByAirlineIndexModel> GetAllAirplaneByAirline();
	AirplaneByAirlineModel GetAirplaneByAirlineById(int id);
	int CreateAirplaneByAirline(AirplaneByAirlineCreateModel airplaneByAirline);
	bool UpdateAirplaneByAirline(AirplaneByAirlineUpdateModel airplaneByAirline);
	bool DeleteAirplaneByAirline(int id);
}