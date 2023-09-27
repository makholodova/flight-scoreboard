using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IFlightService
{
	Task<List<FlightIndexModel>> GetAllFlightsAsync();
	Task<FlightModel> GetFlightByIdAsync(int id);
	Task<int> CreateFlightAsync(FlightCreateModel flight);
	Task<bool> UpdateFlightAsync(FlightUpdateModel flight);
	Task<bool> DeleteFlightAsync(int id);
}