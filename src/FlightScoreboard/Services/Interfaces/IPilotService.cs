using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IPilotService
{
	Task<List<PilotIndexModel>> GetAllPilotsAsync();
	Task<PilotModel> GetPilotByIdAsync(int id);
	Task<bool> UpdatePilotAsync(PilotUpdateModel pilot);
	Task<int> CreatePilotAsync(PilotCreateModel pilot);
	Task<bool> DeletePilotAsync(int id);
}