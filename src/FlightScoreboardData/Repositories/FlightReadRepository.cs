using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IFlightReadRepository
{
	Task<Flight> GetFlightAsync(int id);
	Task<List<FlightDetails>> GetFlightsWithDetailsAsync();
	Task<FlightDetails> GetFlightWithDetailsAsync(int id);

	Task<bool> DoesCityUsed(int id);
	Task<bool> DoesPilotUsed(int id);
	//Task<List<FlightMainInfo>> GetFlightsWithMainInfoAsync(int? toCityId, int? fromCityId, DateTime? departureDateTime, DateTime? arrivalDateTime);
}

public class FlightReadRepository : IFlightReadRepository
{
	private readonly FlightScoreboardContext _context;

	public FlightReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<Flight> GetFlightAsync(int id)
	{
		return await _context.Flights.FindAsync(id);
	}

	public async Task<List<FlightDetails>> GetFlightsWithDetailsAsync()
	{
		return await _context.Flights.Select(GetFlightDetailsConverter()).ToListAsync();
	}

	public async Task<FlightDetails> GetFlightWithDetailsAsync(int id)
	{
		return await _context.Flights.Select(GetFlightDetailsConverter()).FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<bool> DoesCityUsed(int id)
	{
		return await _context.Flights.AnyAsync(x => x.FromCityId == id || x.ToCityId == id );
	}
	public async Task<bool> DoesPilotUsed(int id)
	{
		return await _context.Flights.AnyAsync(x => x.PilotId == id  );
	}


	/*public async Task<List<FlightMainInfo>> GetFlightsWithMainInfoAsync(int? toCityId, int? fromCityId, DateTime? departureDateTime,
		DateTime? arrivalDateTime)
	{
		var flights = _context.Flights.AsQueryable();

		if (toCityId != null)
			flights = flights.Where(x => x.ToCityId == toCityId);

		if (fromCityId != null)
			flights = flights.Where(p => p.FromCityId == fromCityId);

		if (departureDateTime != null)
			flights = flights.Where(x => x.DepartureTime.Date == departureDateTime);

		if (arrivalDateTime != null)
			flights = flights.Where(p => p.ArrivalTime.Date == arrivalDateTime);

		return await flights.Select(p => new FlightMainInfo
		{
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			NumberOfFlight = p.NumberOfFlight,
			FromCity = p.FromCity.Name,
			ToCity = p.ToCity.Name,
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
	}*/

	private static Expression<Func<Flight, FlightDetails>> GetFlightDetailsConverter()
	{
		return flight => new FlightDetails
		{
			Id = flight.Id,
			ArrivalTime = flight.ArrivalTime,
			ActualDepartureTime = flight.ActualDepartureTime,
			ActualArrivalTime = flight.ActualArrivalTime,
			CheckInStartTime = flight.CheckInStartTime,
			CheckInEndTime = flight.CheckInEndTime,
			BoardingStartTime = flight.BoardingStartTime,
			BoardingEndTime = flight.BoardingEndTime,
			NumberOfFlight = flight.NumberOfFlight,
			ToGate = flight.ToGate,
			ToTerminal = flight.ToTerminal,
			FromGate = flight.FromGate,
			FromTerminal = flight.FromTerminal,
			DepartureTime = flight.DepartureTime,
			FromCityId = flight.FromCityId,
			FromCity = flight.FromCity.Name,
			ToCityId = flight.ToCityId,
			ToCity = flight.ToCity.Name,
			PilotId = flight.PilotId,
			PilotFullName = flight.Pilot.Name + " " + flight.Pilot.SurName,
			AirlineId = flight.AirlineId,
			AirlineName = flight.Airline.Name,
			AirplaneId = flight.AirlineAirplaneId,
			AirplaneModel = flight.AirlineAirplane.Airplane.Model,
			AirplaneSerialNumber = flight.AirlineAirplane.SerialNumber
		};
	}
}