using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public interface IAirlineService
{
	Task<List<AirlineModel>> GetAllAirlinesAsync();

	Task<AirlineModel> GetArlineByIdAsync(int id);

	/*Task<List<AirlineShortInfoModel>> GetAvailableAirlinesAsync();*/
	Task<int> CreateAirlineAsync(AirlineCreateModel airline);
	Task<bool> UpdateArlineAsync(AirlineUpdateModel airline);
	Task<bool> DeleteAirlineAsync(int id);
}

public class AirlineService : IAirlineService
{
	private readonly FlightScoreboardContext _context;

	public AirlineService(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<List<AirlineModel>> GetAllAirlinesAsync()
	{
		return await _context.Airlines.Select(p => new AirlineModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToListAsync();
	}

	public async Task<AirlineModel> GetArlineByIdAsync(int id)
	{
		var airline = await _context.Airlines.FirstOrDefaultAsync(p => p.Id == id);
		if (airline == null) return null;
		var airlineModel = new AirlineModel
		{
			Id = airline.Id,
			Name = airline.Name
		};
		return airlineModel;
	}

	/*public async Task<List<AirlineShortInfoModel>> GetAvailableAirlinesAsync()
	{
		return await _context.Airlines.Select(p => new AirlineShortInfoModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToListAsync();
	}*/

	public async Task<int> CreateAirlineAsync(AirlineCreateModel airline)
	{
		var addAirline = await _context.Airlines.AddAsync(new Airline
		{
			Name = airline.Name
		});
		await _context.SaveChangesAsync();
		return addAirline.Entity.Id;
	}

	public async Task<bool> UpdateArlineAsync(AirlineUpdateModel airline)
	{
		var airlineDb = await _context.Airlines.FirstOrDefaultAsync(p => p.Id == airline.Id);
		if (airlineDb == null) return false;
		airlineDb.Name = airline.Name;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirlineAsync(int id)
	{
		var airline = await _context.Airlines.FirstOrDefaultAsync(p => p.Id == id);
		if (airline == null || airline.AirlineAirplanes.Any() || airline.Pilots.Any() || airline.Flights.Any())
			return false;

		_context.Airlines.Remove(airline);

		await _context.SaveChangesAsync();
		return true;
	}
}