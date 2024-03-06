using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IPilotReadRepository
{
	Task<Pilot> GetPilotAsync(int id);
	Task<List<PilotDetails>> GetPilotsWithDetailsAsync(int? airlineId = null);
	Task<PilotDetails> GetPilotWithDetailsAsync(int id);
	Task<bool> DoesAirlineUsed(int id);
}

public class PilotReadRepository : IPilotReadRepository
{
	private readonly FlightScoreboardContext _context;

	public PilotReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<Pilot> GetPilotAsync(int id)
	{
		return await _context.Pilots.FindAsync(id);
	}

	public async Task<List<PilotDetails>> GetPilotsWithDetailsAsync(int? airlineId = null)
	{
		var pilots = _context.Pilots.AsQueryable();
		if (airlineId != null) pilots = pilots.Where(x => x.AirlineId == airlineId);

		return await pilots.Select(GetPilotDetailsConverter()).ToListAsync();
	}

	public async Task<PilotDetails> GetPilotWithDetailsAsync(int id)
	{
		var pilot = await _context.Pilots.Select(GetPilotDetailsConverter())
			.FirstOrDefaultAsync(p => p.Id == id);
		return pilot;
	}

	public async Task<bool> DoesAirlineUsed(int id)
	{
		return await _context.Pilots.AnyAsync(x => x.AirlineId == id);
	}

	private static Expression<Func<Pilot, PilotDetails>> GetPilotDetailsConverter()
	{
		return p => new PilotDetails
		{
			Id = p.Id,
			Name = p.Name,
			SurName = p.SurName,
			Age = p.Age,
			AirlineId = p.AirlineId,
			AirlineName = p.Airline.Name
		};
	}
}