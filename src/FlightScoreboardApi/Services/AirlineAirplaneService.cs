using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface IAirlineAirplaneService
{
	Task<int> CreateAirplaneAsync(int airlineId, AirlineAirplaneCreateModel airplane);
	Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirlineAirplaneService : IAirlineAirplaneService
{
	private readonly IAirlineAirplaneReadRepository _airlineAirplaneReadRepository;
	private readonly IAirlineAirplaneWriteRepository _airlineAirplaneWriteRepository;
	private readonly IFlightReadRepository _flightReadRepository;

	public AirlineAirplaneService(IAirlineAirplaneWriteRepository airlineAirplaneWriteRepository,
		IAirlineAirplaneReadRepository airlineAirplaneReadRepository, IFlightReadRepository flightReadRepository)
	{
		_airlineAirplaneWriteRepository = airlineAirplaneWriteRepository;
		_airlineAirplaneReadRepository = airlineAirplaneReadRepository;
		_flightReadRepository = flightReadRepository;
	}

	public async Task<int> CreateAirplaneAsync(int airlineId, AirlineAirplaneCreateModel airplane)
	{
		return await _airlineAirplaneWriteRepository.CreateAirplaneAsync(new AirlineAirplane
		{
			SerialNumber = airplane.SerialNumber,
			AirlineId = airlineId,
			AirplaneId = airplane.AirplaneId
		});
	}

	public async Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane)
	{
		var airplaneNew = await _airlineAirplaneReadRepository.GetAirplaneAirlineAsync(airplane.Id);
		if (airplaneNew == null) return false;

		airplaneNew.SerialNumber = airplane.SerialNumber;
		airplaneNew.AirplaneId = airplane.AirplaneId;

		return await _airlineAirplaneWriteRepository.UpdateAirplaneAsync(airplaneNew);
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		if (await _flightReadRepository.DoesAirplaneUsed(id)) return false;
		return await _airlineAirplaneWriteRepository.DeleteAirplaneAsync(id);
	}
}