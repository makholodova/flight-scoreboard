using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IPilotService
{
	List<PilotIndexModel> GetAllPilot();
	PilotModel GetPilotById(int id);
	bool UpdatePilot(PilotUpdateModel pilot);
	int CreatePilot(PilotCreateModel pilot);
	bool DeletePilot(int id);
}