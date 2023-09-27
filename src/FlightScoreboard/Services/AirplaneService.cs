using System.Diagnostics.CodeAnalysis;
using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public class AirplaneService : IAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirplaneService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public Task<List<AirplaneModel>> GetAllAirplanesAsync()
	{
		return this._context.Airplanes.Select(p => new AirplaneModel
		{
			Id = p.Id,
			Model = p.Model
		}).ToListAsync();
	}


	public async Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane)
	{
		var addAirplane =await this._context.Airplanes.AddAsync(new Airplane { Model = airplane.Model });
		await this._context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane =await this._context.Airplanes.FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null||airplane.AirlineAirplanes.Any()) return false;

		this._context.Remove(airplane);
		await this._context.SaveChangesAsync();

		return true;
	}
}