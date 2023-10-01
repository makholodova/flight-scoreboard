using System.Diagnostics.CodeAnalysis;
using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
public interface ICityService
{
    Task<List<CityModel>> GetAllCitiesAsync();
    Task<int> CreateCityAsync(CityCreateModel cityNew);
    Task<bool> DeleteCityAsync(int id);
}

public class CityService : ICityService
{
    private readonly FlightScoreboardContext _context;

    public CityService(FlightScoreboardContext context)
    {
        _context = context;
    }

    public async Task<List<CityModel>> GetAllCitiesAsync()
    {
        return await _context.Cities.Select(p => new CityModel
        {
            Id = p.Id,
            Name = p.Name
        }).ToListAsync();
    }

    public async Task<int> CreateCityAsync(CityCreateModel cityNew)
    {
        var addCity = await _context.Cities.AddAsync(new City { Name = cityNew.Name });
        await _context.SaveChangesAsync();

        return addCity.Entity.Id;
    }

    public async Task<bool> DeleteCityAsync(int id)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(p => p.Id == id);

        if (city == null || city.FromFlights.Any() || city.ToFlights.Any())
            return false;

        _context.Cities.Remove(city);

        await _context.SaveChangesAsync();

        return true;
    }
}