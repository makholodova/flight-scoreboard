using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

public class FlightService : IFlightService
{
    private readonly FlightScoreboardContext _context;

    public FlightService(FlightScoreboardContext context)
    {
        _context = context;
    }

    public async Task<List<FlightIndexModel>> GetAllFlightsAsync()
    {
        return await _context.Flights.Select(p => new FlightIndexModel
        {
            Id = p.Id,
            ArrivalTime = p.ArrivalTime,
            DepartureTime = p.DepartureTime,
            FromCity = p.FromCity.Name,
            ToCity = p.ToCity.Name,
            PilotFullName = p.Pilot.Name + " " + p.Pilot.SurName,
            PilotId = p.PilotId,
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplaneId,
            AirplaneSerialNumber = p.AirlineAirplane.SerialNumber
        }).ToListAsync();
    }

    public async Task<FlightModel> GetFlightByIdAsync(int id)
    {
        var flightDb = await _context.Flights.FirstOrDefaultAsync(p => p.Id == id);
        if (flightDb == null) return null;
        return new FlightModel
        {
            Id = flightDb.Id,
            ArrivalTime = flightDb.ArrivalTime,
            DepartureTime = flightDb.DepartureTime,
            FromCityId = flightDb.FromCityId,
            ToCityId = flightDb.ToCityId,
            PilotId = flightDb.PilotId,
            AirlineId = flightDb.AirlineId,
            AirlineAirplaneId = flightDb.AirlineAirplaneId
        };
    }

    public async Task<int> CreateFlightAsync(FlightCreateModel flight)
    {
        var addFlight = await _context.Flights.AddAsync(new Flight
        {
            ArrivalTime = flight.ArrivalTime,
            DepartureTime = flight.DepartureTime,
            FromCityId = flight.FromCityId,
            ToCityId = flight.ToCityId,
            PilotId = flight.PilotId,
            AirlineId = flight.AirlineId,
            AirlineAirplaneId = flight.AirlineAirplaneId
        });
        await _context.SaveChangesAsync();
        return addFlight.Entity.Id;
    }

    public async Task<bool> UpdateFlightAsync(FlightUpdateModel flight)
    {
        var flightNew = await _context.Flights.FirstOrDefaultAsync(p => p.Id == flight.Id);
        if (flightNew == null) return false;

        flightNew.ArrivalTime = flight.ArrivalTime;
        flightNew.DepartureTime = flight.DepartureTime;
        flightNew.FromCityId = flight.FromCityId;
        flightNew.ToCityId = flight.ToCityId;
        flightNew.PilotId = flight.PilotId;
        flightNew.AirlineId = flight.AirlineId;
        flightNew.AirlineAirplaneId = flight.AirlineAirplaneId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FirstOrDefaultAsync(p => p.Id == id);
        if (flight == null) return false;

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return true;
    }
}