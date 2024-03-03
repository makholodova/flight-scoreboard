using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

public interface IFlightService
{
	/*Task<List<FlightIndexModel>> GetAllFlightsAsync(FlightIndexFilterModel flight);*/
	Task<List<FlightIndexModel>> GetAllFlightsAsync();
	Task<FlightIndexModel> GetFlightByIdAsync(int id);
	//Task<FlightModel> GetFlightByIdAsync(int id);
	Task<int> CreateFlightAsync(FlightCreateModel flight);
	Task<bool> UpdateFlightAsync(FlightUpdateModel flight);
	Task<bool> DeleteFlightAsync(int id);
}

public class FlightService : IFlightService
{
	private readonly FlightScoreboardContext _context;

	public FlightService(FlightScoreboardContext context)
	{
		_context = context;
	}

	/*
	public async Task<List<FlightIndexModel>> GetAllFlightsAsync(FlightIndexFilterModel flight)
	{
		var flights = _context.Flights.AsQueryable();


		if (flight.DepartureTime != null)
		{
			var departureTimeMin = flight.DepartureTime.Value.Date;
			var departureTimeMax = departureTimeMin.AddDays(1);
			flights = flights.Where(x => x.DepartureTime >= departureTimeMin && x.DepartureTime < departureTimeMax);
		}

		if (flight.ArrivalTime != null)
		{
			var arrivalTimeMin = flight.ArrivalTime.Value.Date;
			var arrivalDateMax = arrivalTimeMin.AddDays(1);
			flights = flights.Where(x => x.ArrivalTime >= arrivalTimeMin && x.ArrivalTime < arrivalDateMax);
		}

		if (flight.PilotId != null) flights = flights.Where(x => x.PilotId == flight.PilotId);
		if (flight.AirlineId != null) flights = flights.Where(x => x.AirlineId == flight.AirlineId);
		if (flight.AirplaneId != null) flights = flights.Where(x => x.AirlineAirplaneId == flight.AirplaneId);
		if (flight.FromCityId != null) flights = flights.Where(x => x.FromCityId == flight.FromCityId);
		if (flight.ToCityId != null) flights = flights.Where(x => x.ToCityId == flight.ToCityId);
		if (flight.NumberOfFlight != null) flights = flights.Where(x => x.NumberOfFlight == flight.NumberOfFlight);


		return await flights.Select(p => new FlightIndexModel
		{
			Id = p.Id,
			ArrivalTime = p.ArrivalTime,
			ActualDepartureTime = p.ActualDepartureTime,
			ActualArrivalTime = p.ActualArrivalTime,
			CheckInStartTime = p.CheckInStartTime,
			CheckInEndTime = p.CheckInEndTime,
			BoardingStartTime = p.BoardingStartTime,
			BoardingEndTime = p.BoardingEndTime,
			NumberOfFlight = p.NumberOfFlight,
			ToGate = p.ToGate,
			ToTerminal = p.ToTerminal,
			FromGate = p.FromGate,
			FromTerminal = p.FromTerminal,
			DepartureTime = p.DepartureTime,
			FromCity = p.FromCity.Name,
			ToCity = p.ToCity.Name,
			PilotFullName = p.Pilot.Name + " " + p.Pilot.SurName,
			PilotId = p.PilotId,
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			AirplaneModel = p.AirlineAirplane.Airplane.Model,
			AirplaneId = p.AirlineAirplaneId,
			AirplaneSerialNumber = p.AirlineAirplane.SerialNumber
		}).ToListAsync();
	}
	*/

	public async Task<List<FlightIndexModel>> GetAllFlightsAsync()
	{
		return await _context.Flights.Select(p => new FlightIndexModel
		{
			Id = p.Id,
			ArrivalTime = p.ArrivalTime,
			ActualDepartureTime = p.ActualDepartureTime,
			ActualArrivalTime = p.ActualArrivalTime,
			CheckInStartTime = p.CheckInStartTime,
			CheckInEndTime = p.CheckInEndTime,
			BoardingStartTime = p.BoardingStartTime,
			BoardingEndTime = p.BoardingEndTime,
			NumberOfFlight = p.NumberOfFlight,
			ToGate = p.ToGate,
			ToTerminal = p.ToTerminal,
			FromGate = p.FromGate,
			FromTerminal = p.FromTerminal,
			DepartureTime = p.DepartureTime,
			FromCity = p.FromCity.Name,
			FromCityId = p.FromCityId,
			ToCity = p.ToCity.Name,
			ToCityId = p.ToCityId,
			PilotFullName = p.Pilot.Name + " " + p.Pilot.SurName,
			PilotId = p.PilotId,
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			AirplaneModel = p.AirlineAirplane.Airplane.Model,
			AirplaneId = p.AirlineAirplaneId,
			AirplaneSerialNumber = p.AirlineAirplane.SerialNumber
		}).ToListAsync();
	}


	public async Task<FlightIndexModel> GetFlightByIdAsync(int id)
	{
		var flightDb = await _context.Flights.Include(flight => flight.FromCity).FirstOrDefaultAsync(p => p.Id == id);
		if (flightDb == null) return null;
		return new FlightIndexModel
		{
			Id = flightDb.Id,
			ArrivalTime = flightDb.ArrivalTime,
			ActualDepartureTime = flightDb.ActualDepartureTime,
			ActualArrivalTime = flightDb.ActualArrivalTime,
			CheckInStartTime = flightDb.CheckInStartTime,
			CheckInEndTime = flightDb.CheckInEndTime,
			BoardingStartTime = flightDb.BoardingStartTime,
			BoardingEndTime = flightDb.BoardingEndTime,
			NumberOfFlight = flightDb.NumberOfFlight,
			ToGate = flightDb.ToGate,
			ToTerminal = flightDb.ToTerminal,
			FromGate = flightDb.FromGate,
			FromTerminal = flightDb.FromTerminal,
			DepartureTime = flightDb.DepartureTime,
			FromCity = flightDb.FromCity.Name,
			ToCity = flightDb.ToCity.Name,
			PilotFullName = flightDb.Pilot.Name + " " + flightDb.Pilot.SurName,
			PilotId = flightDb.PilotId,
			AirlineName = flightDb.Airline.Name,
			AirlineId = flightDb.AirlineId,
			AirplaneModel = flightDb.AirlineAirplane.Airplane.Model,
			AirplaneId = flightDb.AirlineAirplaneId,
			AirplaneSerialNumber = flightDb.AirlineAirplane.SerialNumber
		};


		/*return new FlightModel

		{
			Id = flightDb.Id,
			ArrivalTime = flightDb.ArrivalTime,
			ActualDepartureTime = flightDb.ActualDepartureTime,
			ActualArrivalTime = flightDb.ActualArrivalTime,
			CheckInStartTime = flightDb.CheckInStartTime,
			CheckInEndTime = flightDb.CheckInEndTime,
			BoardingStartTime = flightDb.BoardingStartTime,
			BoardingEndTime = flightDb.BoardingEndTime,
			NumberOfFlight = flightDb.NumberOfFlight,
			ToGate = flightDb.ToGate,
			ToTerminal = flightDb.ToTerminal,
			FromGate = flightDb.FromGate,
			FromTerminal = flightDb.FromTerminal,
			DepartureTime = flightDb.DepartureTime,
			FromCityId = flightDb.FromCityId,
			ToCityId = flightDb.ToCityId,
			PilotId = flightDb.PilotId,
			AirlineId = flightDb.AirlineId,
			AirlineAirplaneId = flightDb.AirlineAirplaneId
		};*/
	}

	public async Task<int> CreateFlightAsync(FlightCreateModel flight)
	{
		var addFlight = await _context.Flights.AddAsync(new Flight
		{
			DepartureTime =flight.DepartureTime, //DateTime.Now
			ArrivalTime = flight.ArrivalTime,//DateTime.Now
			ActualArrivalTime = flight.ActualArrivalTime,
			ActualDepartureTime = flight.ActualDepartureTime,
			CheckInStartTime = flight.CheckInStartTime,
			CheckInEndTime = flight.CheckInEndTime,
			BoardingStartTime = flight.BoardingStartTime,
			BoardingEndTime = flight.BoardingEndTime,
			NumberOfFlight = flight.NumberOfFlight,
			ToGate = flight.ToGate,
			ToTerminal = flight.ToTerminal,
			FromGate = flight.FromGate,
			FromTerminal = flight.FromTerminal,
			FromCityId = flight.FromCityId,
			ToCityId = flight.ToCityId,
			PilotId = flight.PilotId,
			AirlineId = flight.AirlineId,
			AirlineAirplaneId = flight.AirlineAirplaneId
		});
		await _context.SaveChangesAsync();
		return addFlight.Entity.Id;
	}

	public async Task<bool> UpdateFlightAsync(FlightUpdateModel flight)
	{
		var flightNew = await _context.Flights.FirstOrDefaultAsync(p => p.Id == flight.Id);
		if (flightNew == null) return false;

		flightNew.ArrivalTime = flight.ArrivalTime;
		flightNew.DepartureTime = flight.DepartureTime;
		flightNew.FromCityId = flight.FromCityId;
		flightNew.ToCityId = flight.ToCityId;
		flightNew.PilotId = flight.PilotId;
		flightNew.AirlineId = flight.AirlineId;
		flightNew.AirlineAirplaneId = flight.AirplaneId;
		flightNew.NumberOfFlight = flight.NumberOfFlight;
		flightNew.ToTerminal = flight.ToTerminal;
		flightNew.ToGate = flight.ToGate;
		flightNew.FromTerminal = flight.FromTerminal;
		flightNew.FromGate = flight.FromGate;
		flightNew.ActualArrivalTime = flight.ActualArrivalTime;
		flightNew.ActualDepartureTime = flight.ActualDepartureTime;
		flightNew.CheckInStartTime = flight.CheckInStartTime;
		flightNew.CheckInEndTime = flight.CheckInEndTime;
		flightNew.BoardingStartTime = flight.BoardingStartTime;
		flightNew.BoardingEndTime = flight.BoardingEndTime;

		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteFlightAsync(int id)
	{
		var flight = await _context.Flights.FirstOrDefaultAsync(p => p.Id == id);
		if (flight == null) return false;

		_context.Flights.Remove(flight);
		await _context.SaveChangesAsync();
		return true;
	}
}