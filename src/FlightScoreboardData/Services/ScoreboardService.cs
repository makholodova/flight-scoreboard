using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

public interface IScoreboardService
{
	Task<List<ScoreboardDepartureIndexModel>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime);
	Task<List<ScoreboardArrivalIndexModel>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime);
}

public class ScoreboardService : IScoreboardService

{
	private readonly FlightScoreboardContext _context;
	private readonly IStatusService _statusService;

	public ScoreboardService(FlightScoreboardContext context, IStatusService statusService)
	{
		_context = context;
		_statusService = statusService;
	}

	public async Task<List<ScoreboardDepartureIndexModel>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime)
	{
		var flights = _context.Flights.AsQueryable();

		if (cityId != null) flights = flights.Where(x => x.ToCityId == cityId);

		if (dateTime != null)
			flights = flights.Where(x => x.DepartureTime.Date == dateTime);

		var scoreboardDeparture = await flights.Select(p => new ScoreboardDepartureIndexModel
		{
			ArrivalTime = p.ArrivalTime,
			DepartureTime = p.DepartureTime,
			ToCity = p.ToCity.Name,
			NumberOfFlight = p.NumberOfFlight,
			Gate = p.FromGate,
			Terminal = p.FromTerminal,
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			AirplaneModel = p.AirlineAirplane.Airplane.Model,
			AirplaneId = p.AirlineAirplaneId,
			ActualDepartureTime = p.ActualDepartureTime,
			ActualArrivalTime = p.ActualArrivalTime,
			CheckInStartTime = p.CheckInStartTime,
			CheckInEndTime = p.CheckInEndTime,
			BoardingStartTime = p.BoardingStartTime,
			BoardingEndTime = p.BoardingEndTime
		}).ToListAsync();
		foreach (var scoreboard in scoreboardDeparture)
			scoreboard.StatusMessage = _statusService.CalculateStatus(scoreboard);
		return scoreboardDeparture;
	}

	public async Task<List<ScoreboardArrivalIndexModel>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime)
	{
		var flights = _context.Flights.AsQueryable();
		if (cityId != null) flights = flights.Where(p => p.FromCityId == cityId);
		if (dateTime != null) flights = flights.Where(p => p.ArrivalTime.Date == dateTime);

		var scoreboardArrival = await flights.Select(p => new ScoreboardArrivalIndexModel
		{
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			NumberOfFlight = p.NumberOfFlight,
			FromCity = p.FromCity.Name,
			ArrivalTime = p.ArrivalTime,
			DepartureTime = p.DepartureTime,
			AirplaneModel = p.AirlineAirplane.Airplane.Model,
			AirplaneId = p.AirlineAirplane.AirplaneId,
			Terminal = p.ToTerminal,
			Gate = p.ToGate,
			ActualDepartureTime = p.ActualDepartureTime,
			ActualArrivalTime = p.ActualArrivalTime,
			CheckInStartTime = p.CheckInStartTime,
			CheckInEndTime = p.CheckInEndTime,
			BoardingStartTime = p.BoardingStartTime,
			BoardingEndTime = p.BoardingEndTime
		}).ToListAsync();

		foreach (var scoreboard in scoreboardArrival)
			scoreboard.StatusMessage = _statusService.CalculateStatus(scoreboard);

		return scoreboardArrival;
	}
}