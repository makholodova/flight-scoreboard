using System.Diagnostics.CodeAnalysis;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public interface IAirlineAirplaneService
{
	Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync(int airlineId);
	Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync();
	Task<AirlineAirplaneModel> GetAirplaneAirlineByIdAsync(int id);
	Task<int> CreateAirplaneAsync(AirlineAirplaneCreateModel airplane);
	Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirlineAirplaneService : IAirlineAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirlineAirplaneService(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync(int airlineId)
	{
		return await _context.AirlineAirplanes.Where(p => p.AirlineId == airlineId).Select(p =>
			new AirlineAirplaneShortModel
			{
				Id = p.Id,
				SerialNumber = p.SerialNumber,
				AirlineId = p.AirlineId,
				AirlineName = p.Airline.Name,
				AirplaneId = p.AirplaneId,
				AirplaneModel = p.Airplane.Model
			}).ToListAsync();
	}

	public async Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync()
	{
		return await _context.AirlineAirplanes.Select(p =>
			new AirlineAirplaneShortModel
			{
				Id = p.Id,
				SerialNumber = p.SerialNumber,
				AirlineId = p.AirlineId,
				AirlineName = p.Airline.Name,
				AirplaneId = p.AirplaneId,
				AirplaneModel = p.Airplane.Model
			}).ToListAsync();
	}


	public async Task<AirlineAirplaneModel> GetAirplaneAirlineByIdAsync(int id) // ?необходимость
	{
		var airplane = await _context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null) return null;

		return new AirlineAirplaneModel
		{
			Id = airplane.Id,
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirplaneId = airplane.AirplaneId
		};
	}

	public async Task<int> CreateAirplaneAsync(AirlineAirplaneCreateModel airplane)
	{
		var addAirplane = await _context.AddAsync(new AirlineAirplane
		{
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirplaneId = airplane.AirplaneId
		});

		await _context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane)
	{
		var airplaneDb = await _context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == airplane.Id);
		if (airplaneDb == null) return false;

		airplaneDb.SerialNumber = airplane.SerialNumber;
		airplaneDb.AirlineId = airplane.AirlineId;
		airplaneDb.AirplaneId = airplane.AirplaneId;

		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane = await _context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null || airplane.Flights.Any()) return false;

		_context.Remove(airplane);
		await _context.SaveChangesAsync();
		return true;
	}
}