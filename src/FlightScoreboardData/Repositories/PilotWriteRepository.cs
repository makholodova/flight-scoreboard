using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IPilotWriteRepository
{
	Task<int> CreatePilotAsync(Pilot pilot);
	Task<bool> UpdatePilotAsync(Pilot pilot);
	Task<bool> DeletePilotAsync(int id);
}

public class PilotWriteRepository : IPilotWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public PilotWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreatePilotAsync(Pilot pilot)
	{
		var addPilot = await _context.Pilots.AddAsync(pilot);
		await _context.SaveChangesAsync();
		return addPilot.Entity.Id;
	}

	public async Task<bool> UpdatePilotAsync(Pilot pilot)
	{
		_context.Entry(pilot).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeletePilotAsync(int id)
	{
		var pilot = await _context.Pilots.FindAsync(id);
		if (pilot == null) return false;

		_context.Pilots.Remove(pilot);
		await _context.SaveChangesAsync();
		return true;
	}
}