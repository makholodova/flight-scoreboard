using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirlineAirplaneWriteRepository
{
	Task<int> CreateAirplaneAsync(AirlineAirplane airplane);

	Task<bool> UpdateAirplaneAsync(AirlineAirplane airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirlineAirplaneWriteRepository : IAirlineAirplaneWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public AirlineAirplaneWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreateAirplaneAsync(AirlineAirplane airplane)
	{
		var addAirplane = await _context.AddAsync(airplane);
		await _context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> UpdateAirplaneAsync(AirlineAirplane airplane)
	{
		_context.Entry(airplane).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane = await _context.AirlineAirplanes.FindAsync(id);
		if (airplane == null) return false;

		_context.Remove(airplane);
		await _context.SaveChangesAsync();
		return true;
	}
}