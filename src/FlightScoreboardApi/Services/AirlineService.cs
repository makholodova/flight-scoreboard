using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface IAirlineService
{
	Task<int> CreateAirlineAsync(AirlineCreateModel airline);
	Task<bool> UpdateArlineAsync(AirlineUpdateModel airline);
	Task<bool> DeleteAirlineAsync(int id);
}

public class AirlineService : IAirlineService
{
	private readonly IAirlineAirplaneReadRepository _airlineAirplaneReadRepository;
	private readonly IAirlineReadRepository _airlineReadRepository;
	private readonly IAirlineWriteRepository _airlineWriteRepository;
	private readonly IFlightReadRepository _flightReadRepository;
	private readonly IPilotReadRepository _pilotReadRepository;

	public AirlineService(IAirlineReadRepository airlineReadRepository,
		IAirlineWriteRepository airlineWriteRepository, IAirlineAirplaneReadRepository airlineAirplaneReadRepository,
		IPilotReadRepository pilotReadRepository, IFlightReadRepository flightReadRepository)
	{
		_airlineReadRepository = airlineReadRepository;
		_airlineWriteRepository = airlineWriteRepository;
		_airlineAirplaneReadRepository = airlineAirplaneReadRepository;
		_pilotReadRepository = pilotReadRepository;
		_flightReadRepository = flightReadRepository;
	}

	public async Task<int> CreateAirlineAsync(AirlineCreateModel airline)
	{
		return await _airlineWriteRepository.CreateAirlineAsync(new Airline
		{
			Name = airline.Name
		});
	}

	public async Task<bool> UpdateArlineAsync(AirlineUpdateModel airline)
	{
		var airlineNew = await _airlineReadRepository.GetArlineAsync(airline.Id);
		if (airlineNew == null) return false;
		airlineNew.Name = airline.Name;
		return await _airlineWriteRepository.UpdateArlineAsync(airlineNew);
	}

	public async Task<bool> DeleteAirlineAsync(int id)
	{
		if (await _airlineAirplaneReadRepository.DoesAirlineUsed(id)
		    || await _pilotReadRepository.DoesAirlineUsed(id)
		    || await _flightReadRepository.DoesAirlineUsed(id))
			return false;
		return await _airlineWriteRepository.DeleteAirlineAsync(id);
	}
}