using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirlineAirplaneReadRepository
{
	Task<AirlineAirplane> GetAirplaneAirlineAsync(int id);
	Task<List<AirlineAirplaneDetails>> GetAirlineAirplanesWithDetailsAsync(int airlineId);
	Task<AirlineAirplaneDetails> GetAirplaneAirlineWithDetailsAsync(int id);
	Task<bool> DoesAirplaneUsed(int id);
	Task<bool> DoesAirlineUsed(int id);
}

public class AirlineAirplaneReadRepository : IAirlineAirplaneReadRepository
{
	private readonly FlightScoreboardContext _context;

	public AirlineAirplaneReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<AirlineAirplane> GetAirplaneAirlineAsync(int id)
	{
		return await _context.AirlineAirplanes.FindAsync(id);
	}

	public async Task<List<AirlineAirplaneDetails>> GetAirlineAirplanesWithDetailsAsync(int airlineId)
	{
		return await _context.AirlineAirplanes.Where(p => p.AirlineId == airlineId).Select(GetAirlineAirplaneDetailsConverter()).ToListAsync();
	}

	public async Task<AirlineAirplaneDetails> GetAirplaneAirlineWithDetailsAsync(int id)
	{
		var airplane = await _context.AirlineAirplanes.Select(GetAirlineAirplaneDetailsConverter())
			.FirstOrDefaultAsync(p => p.Id == id);
		return airplane;
	}

	public async Task<bool> DoesAirplaneUsed(int id)
	{
		return await _context.AirlineAirplanes.AnyAsync(x => x.AirplaneId == id);
	}

	public async Task<bool> DoesAirlineUsed(int id)
	{
		return await _context.AirlineAirplanes.AnyAsync(x => x.AirlineId == id);
	}

	private static Expression<Func<AirlineAirplane, AirlineAirplaneDetails>> GetAirlineAirplaneDetailsConverter()
	{
		return p =>
			new AirlineAirplaneDetails
			{
				Id = p.Id,
				SerialNumber = p.SerialNumber,
				AirlineId = p.AirlineId,
				AirlineName = p.Airline.Name,
				AirplaneId = p.AirplaneId,
				AirplaneModel = p.Airplane.Model
			};
	}
}