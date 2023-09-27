using System.Diagnostics.CodeAnalysis;
using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public class AirlineAirplaneService : IAirlineAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirlineAirplaneService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public async Task<List<AirlineAirplaneShortModel>> GetAllAirlineAirplanesAsync(int airlineId)
	{
		return await this._context.AirlineAirplanes.Where(p => p.AirlineId == airlineId).Select(p =>
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
		return await this._context.AirlineAirplanes.Select(p =>
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
		var airplane =await this._context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == id);
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
		var addAirplane =await this._context.AddAsync(new AirlineAirplane
		{
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirplaneId = airplane.AirplaneId
		});

		await this._context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> UpdateAirplaneAsync(AirlineAirplaneUpdateModel airplane)
	{
		var airplaneDb =await this._context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == airplane.Id);
		if (airplaneDb == null) return false;

		airplaneDb.SerialNumber = airplane.SerialNumber;
		airplaneDb.AirlineId = airplane.AirlineId;
		airplaneDb.AirplaneId = airplane.AirplaneId;

		await this._context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane = await this._context.AirlineAirplanes.FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null|| airplane.Flights.Any()) return false;

		this._context.Remove(airplane);
		await this._context.SaveChangesAsync();
		return true;
	}
}