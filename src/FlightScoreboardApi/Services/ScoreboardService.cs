using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightScoreboardData.Repositories;
using FlightScoreboardData.Repositories.Models;

namespace FlightScoreboardApi.Services;

public interface IScoreboardService
{
	Task<List<FlightMainInfo>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime);
	Task<List<FlightMainInfo>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime);
}

public class ScoreboardService : IScoreboardService

{
	private readonly IFlightReadRepository _flightReadRepository;


	private readonly IStatusService _statusService;

	public ScoreboardService(IStatusService statusService, IFlightReadRepository flightReadRepository)
	{
		_statusService = statusService;
		_flightReadRepository = flightReadRepository;
	}

	public async Task<List<FlightMainInfo>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime)
	{
		var flights = await _flightReadRepository.GetFlightsWithMainInfoAsync(cityId, null, dateTime, null);

		foreach (var flight in flights)
			flight.StatusMessage = _statusService.CalculateStatus(flight);
		return flights;
	}

	public async Task<List<FlightMainInfo>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime)
	{
		var flights = await _flightReadRepository.GetFlightsWithMainInfoAsync(null, cityId, null, dateTime);

		foreach (var flight in flights)
			flight.StatusMessage = _statusService.CalculateStatus(flight);

		return flights;
	}
}