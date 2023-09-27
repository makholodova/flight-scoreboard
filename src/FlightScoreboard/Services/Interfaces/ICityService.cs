using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface ICityService
{
	Task<List<CityModel>>  GetAllCitiesAsync();
	Task<int> CreateCityAsync(CityCreateModel cityNew);
	Task<bool> DeleteCityAsync(int id);
	
}