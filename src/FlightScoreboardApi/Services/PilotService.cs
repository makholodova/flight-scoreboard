using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface IPilotService
{
	Task<int> CreatePilotAsync(PilotCreateModel pilot);
	Task<bool> UpdatePilotAsync(PilotUpdateModel pilot);
	Task<bool> DeletePilotAsync(int id);
}

public class PilotService : IPilotService
{
	private readonly IFlightReadRepository _flightReadRepository;
	private readonly IPilotReadRepository _pilotReadRepository;
	private readonly IPilotWriteRepository _pilotWriteRepository;

	public PilotService(IPilotWriteRepository pilotWriteRepository, IFlightReadRepository flightReadRepository,
		IPilotReadRepository pilotReadRepository)
	{
		_pilotWriteRepository = pilotWriteRepository;
		_flightReadRepository = flightReadRepository;
		_pilotReadRepository = pilotReadRepository;
	}

	public async Task<int> CreatePilotAsync(PilotCreateModel pilot)
	{
		return await _pilotWriteRepository.CreatePilotAsync(new Pilot
			{
				Name = pilot.Name,
				SurName = pilot.SurName,
				Age = pilot.Age,
				AirlineId = pilot.AirlineId
			}
		);
	}

	public async Task<bool> UpdatePilotAsync(PilotUpdateModel pilot)
	{
		var pilotNew = await _pilotReadRepository.GetPilotAsync(pilot.Id);
		if (pilotNew == null) return false;

		pilotNew.Name = pilot.Name;
		pilotNew.SurName = pilot.SurName;
		pilotNew.Age = pilot.Age;
		pilotNew.AirlineId = pilot.AirlineId;

		await _pilotWriteRepository.UpdatePilotAsync(pilotNew);
		return true;
	}

	public async Task<bool> DeletePilotAsync(int id)
	{
		if (await _flightReadRepository.DoesPilotUsed(id))
			return false;
		return await _pilotWriteRepository.DeletePilotAsync(id);
	}
}