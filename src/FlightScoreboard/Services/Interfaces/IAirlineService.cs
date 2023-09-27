using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineService
{
	Task<List<AirlineModel>> GetAllAirlinesAsync();
	Task<List<AirlineShortInfoModel>> GetAvailableAirlinesAsync();
	Task<int> CreateAirlineAsync(AirlineCreateModel airline);
	Task<bool> DeleteAirlineAsync(int id);
}