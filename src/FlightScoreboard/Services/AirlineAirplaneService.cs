using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;
using AirlineAirplaneCreateModel = FlightScoreboard.Models.AirlineAirplaneCreateModel;
using AirlineAirplaneUpdateModel = FlightScoreboard.Models.AirlineAirplaneUpdateModel;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public interface IAirlineAirplaneService
{
	Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync(int airlineId);
	Task<AirlineAirplaneShortModel> GetAirplaneAirlineByIdAsync(int id);
	Task<int> CreateAirplaneAsync(int airlineId, AirlineAirplaneCreateModel airplane);
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

	public async Task<AirlineAirplaneShortModel> GetAirplaneAirlineByIdAsync(int id)
	{
		var airplane = await _context.AirlineAirplanes.Include(airlineAirplane => airlineAirplane.Airline)
			.Include(airlineAirplane => airlineAirplane.Airplane).FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null) return null;

		return new AirlineAirplaneShortModel
		{
			Id = airplane.Id,
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirlineName = airplane.Airline.Name,
			AirplaneId = airplane.AirplaneId,
			AirplaneModel = airplane.Airplane.Model
		};
	}

	public async Task<int> CreateAirplaneAsync(int airlineId, AirlineAirplaneCreateModel airplane)
	{
		var addAirplane = await _context.AddAsync(new AirlineAirplane
		{
			SerialNumber = airplane.SerialNumber,
			AirlineId = airlineId,
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