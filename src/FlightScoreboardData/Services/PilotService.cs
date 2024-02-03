using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
public interface IPilotService
{
	Task<List<PilotIndexModel>> GetAllPilotsAsync();
	Task<PilotModel> GetPilotByIdAsync(int id);
	Task<bool> UpdatePilotAsync(PilotUpdateModel pilot);
	Task<int> CreatePilotAsync(PilotCreateModel pilot);
	Task<bool> DeletePilotAsync(int id);
}

public class PilotService : IPilotService
{
	private readonly FlightScoreboardContext _context;

	public PilotService(FlightScoreboardContext context)
	{
		_context = context;
	}


	public async Task<List<PilotIndexModel>> GetAllPilotsAsync()
	{
		return await _context.Pilots.Select(p => new PilotIndexModel
		{
			Id = p.Id,
			Name = p.Name,
			SurName = p.SurName,
			Age = p.Age,
			AirlineId = p.AirlineId,
			AirlineName = p.Airline.Name
		}).ToListAsync();
	}


	public async Task<PilotModel> GetPilotByIdAsync(int id)
	{
		var pilot = await _context.Pilots.Include(pilot => pilot.Airline).FirstOrDefaultAsync(p => p.Id == id);
		if (pilot == null) return null;

		var pilotModel = new PilotModel
		{
			Id = pilot.Id,
			Name = pilot.Name,
			SurName = pilot.SurName,
			Age = pilot.Age,
			AirlineId = pilot.AirlineId,
			AirlineName = pilot.Airline.Name
		};
		return pilotModel;
	}

	public async Task<bool> UpdatePilotAsync(PilotUpdateModel pilot)
	{
		var pilotDb = await _context.Pilots.FirstOrDefaultAsync(p => p.Id == pilot.Id);
		if (pilotDb == null) return false;

		pilotDb.Name = pilot.Name;
		pilotDb.SurName = pilot.SurName;
		pilotDb.Age = pilot.Age;
		pilotDb.AirlineId = pilot.AirlineId;
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<int> CreatePilotAsync(PilotCreateModel pilotNew)
	{
		var addPilot = await _context.Pilots.AddAsync(new Pilot
			{
				Name = pilotNew.Name,
				SurName = pilotNew.SurName,
				Age = pilotNew.Age,
				AirlineId = pilotNew.AirlineId
			}
		);
		await _context.SaveChangesAsync();

		return addPilot.Entity.Id;
	}

	public async Task<bool> DeletePilotAsync(int id)
	{
		var pilot = _context.Pilots.FirstOrDefault(p => p.Id == id);
		if (pilot == null || pilot.Flights.Any()) return false;

		_context.Pilots.Remove(pilot);
		await _context.SaveChangesAsync();

		return true;
	}
}