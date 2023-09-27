using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirplaneService
{
	Task<List<AirplaneModel>> GetAllAirplanesAsync();
	Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}