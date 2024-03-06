using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface IAirplaneService
{
	Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane);
	Task<bool> UpdateAirplaneAsync(AirplaneUpdateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirplaneService : IAirplaneService
{
	private readonly IAirlineAirplaneReadRepository _airlineAirplaneReadRepository;
	private readonly IAirplaneReadRepository _airplaneReadRepository;
	private readonly IAirplaneWriteRepository _airplaneWriteRepository;

	public AirplaneService(IAirplaneWriteRepository airplaneWriteRepository, IAirplaneReadRepository airplaneReadRepository,
		IAirlineAirplaneReadRepository airlineAirplaneReadRepository)
	{
		_airplaneWriteRepository = airplaneWriteRepository;
		_airplaneReadRepository = airplaneReadRepository;
		_airlineAirplaneReadRepository = airlineAirplaneReadRepository;
	}


	public async Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane)
	{
		return await _airplaneWriteRepository.CreateAirplaneAsync(
			new Airplane
			{
				Model = airplane.Model
			});
	}

	public async Task<bool> UpdateAirplaneAsync(AirplaneUpdateModel airplane)
	{
		var airplaneNew = await _airplaneReadRepository.GetAirplaneAsync(airplane.Id);
		if (airplaneNew == null) return false;

		airplaneNew.Model = airplane.Model;
		await _airplaneWriteRepository.UpdateAirplaneAsync(airplaneNew);
		return true;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		if (await _airlineAirplaneReadRepository.DoesAirplaneUsed(id)) return false;
		return await _airplaneWriteRepository.DeleteAirplaneAsync(id);
	}
}