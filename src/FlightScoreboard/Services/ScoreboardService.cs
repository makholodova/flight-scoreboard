using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

public interface IScoreboardService
{
    Task<List<ScoreboardIndexModel>> GetAllFlightsAsync(int? cityId, DateTime? dateTime);
}

public class ScoreboardService : IScoreboardService

{
    private readonly FlightScoreboardContext _context;

    public ScoreboardService(FlightScoreboardContext context)
    {
        _context = context;
    }

    public async Task<List<ScoreboardIndexModel>> GetAllFlightsAsync(int? cityId, DateTime? dateTime)
    {
        var flights = _context.Flights.AsQueryable();

        if (cityId != null) flights = flights.Where(x => x.FromCityId == cityId || x.ToCityId == cityId);

        if (dateTime != null)
            flights = flights
                .Where(x => x.ArrivalTime.Date == dateTime || x.DepartureTime.Date == dateTime);

        return await flights.Select(p => new ScoreboardIndexModel
        {
            ArrivalTime = p.ArrivalTime,
            DepartureTime = p.DepartureTime,
            FromCity = p.FromCity.Name,
            ToCity = p.ToCity.Name,
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplaneId
        }).ToListAsync();
    }
}