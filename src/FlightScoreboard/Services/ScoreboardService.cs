using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

public interface IScoreboardService
{
    Task<List<ScoreboardDepartureIndexModel>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime);
    Task<List<ScoreboardArrivalIndexModel>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime);
}

public class ScoreboardService : IScoreboardService

{
    private readonly FlightScoreboardContext _context;

    public ScoreboardService(FlightScoreboardContext context)
    {
        _context = context;
    }

    public async Task<List<ScoreboardDepartureIndexModel>> GetDepartureFlightsAsync(int? cityId, DateTime? dateTime)
    {
        var flights = _context.Flights.AsQueryable();

        if (cityId != null) flights = flights.Where(x => x.ToCityId == cityId);

        if (dateTime != null)
            flights = flights.Where(x => x.DepartureTime.Date == dateTime);

        return await flights.Select(p => new ScoreboardDepartureIndexModel
        {
            DepartureTime = p.DepartureTime,
            ToCity = p.ToCity.Name,
            NumberOfFlight = p.NumberOfFlight,
            Gate = p.FromGate,
            Terminal = p.FromGate,
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplaneId
        }).ToListAsync();
    }

    public async Task<List<ScoreboardArrivalIndexModel>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime)
    {
        var flights = _context.Flights.AsQueryable();
        if (cityId != null) flights = flights.Where(p => p.FromCityId == cityId);
        if (dateTime != null) flights = flights.Where(p => p.ArrivalTime.Date == dateTime);


        return await flights.Select(p => new ScoreboardArrivalIndexModel
        {
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            NumberOfFlight = p.NumberOfFlight,
            FromCity = p.FromCity.Name,
            ArrivalTime = p.ArrivalTime,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplane.AirplaneId,
            Terminal = p.ToTerminal,
            Gate = p.ToGate
        }).ToListAsync();
    }
}