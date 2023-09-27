using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IAirlineAirplaneService
{
	Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync(int airlineId);
	Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync();
	Task<AirlineAirplaneModel> GetAirplaneAirlineByIdAsync(int id);
	Task<int> CreateAirplaneAsync(AirlineAirplaneCreateModel airplane);
	Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}