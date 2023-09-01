using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface ICityService
{
	List<CityModel> GetAllCity();
	int CreateCity(CityCreateModel cityNew);
	bool DeleteCity(int id);
	
}