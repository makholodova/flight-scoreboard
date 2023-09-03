using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface ICityService
{
	List<CityModel> GetAllCities();
	int CreateCity(CityCreateModel cityNew);
	bool DeleteCity(int id);
	
}