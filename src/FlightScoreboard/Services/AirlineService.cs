using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public interface IAirlineService
{
    Task<List<AirlineModel>> GetAllAirlinesAsync();
    Task<List<AirlineShortInfoModel>> GetAvailableAirlinesAsync();
    Task<int> CreateAirlineAsync(AirlineCreateModel airline);
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

    public async Task<List<AirlineShortInfoModel>> GetAvailableAirlinesAsync()
    {
        return await _context.Airlines.Select(p => new AirlineShortInfoModel
        {
            Id = p.Id,
            Name = p.Name
        }).ToListAsync();
    }

    public async Task<int> CreateAirlineAsync(AirlineCreateModel airline)
    {
        var addAirline = await _context.Airlines.AddAsync(new Airline
        {
            Name = airline.Name
        });
        await _context.SaveChangesAsync();
        return addAirline.Entity.Id;
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