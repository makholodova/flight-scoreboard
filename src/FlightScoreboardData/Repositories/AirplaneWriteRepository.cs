using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirplaneWriteRepository
{
	Task<int> CreateAirplaneAsync(Airplane airplane);

	Task<bool> UpdateAirplaneAsync(Airplane airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirplaneWriteRepository : IAirplaneWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public AirplaneWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreateAirplaneAsync(Airplane airplane)
	{
		var addAirplane = await _context.Airplanes.AddAsync(airplane);
		await _context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> UpdateAirplaneAsync(Airplane airplane)
	{
		_context.Entry(airplane).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane = await _context.Airplanes.FindAsync(id);
		if (airplane == null) return false;

		_context.Remove(airplane);
		await _context.SaveChangesAsync();
		return true;
	}
}