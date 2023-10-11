using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

public interface IFlightService
{
    Task<List<FlightIndexModel>> GetAllFlightsAsync(FlightIndexFilterModel flight);
    Task<FlightModel> GetFlightByIdAsync(int id);

    Task<int> CreateFlightAsync(FlightCreateModel flight);

    // Task<int> CreateRepeatEventFlightAsync(FlightCreateRepeatEventModel flight);
    Task<bool> UpdateFlightAsync(FlightUpdateModel flight);
    Task<bool> DeleteFlightAsync(int id);
}

public class FlightService : IFlightService
{
    private readonly FlightScoreboardContext _context;

    public FlightService(FlightScoreboardContext context)
    {
        _context = context;
    }

    public async Task<List<FlightIndexModel>> GetAllFlightsAsync(FlightIndexFilterModel flight)
    {
        var flights = _context.Flights.AsQueryable();


        if (flight.DepartureTime != null)
        {
            var departureTimeMin = flight.DepartureTime.Value.Date;
            var departureTimeMax = departureTimeMin.AddDays(1);
            flights = flights.Where(x => x.DepartureTime >= departureTimeMin && x.DepartureTime < departureTimeMax);
        }

        if (flight.ArrivalTime != null)
        {
            var arrivalTimeMin = flight.ArrivalTime.Value.Date;
            var arrivalDateMax = arrivalTimeMin.AddDays(1);
            flights = flights.Where(x => x.ArrivalTime >= arrivalTimeMin && x.ArrivalTime < arrivalDateMax);
        }

        if (flight.PilotId != null) flights = flights.Where(x => x.PilotId == flight.PilotId);
        if (flight.AirlineId != null) flights = flights.Where(x => x.AirlineId == flight.AirlineId);
        if (flight.AirplaneId != null) flights = flights.Where(x => x.AirlineAirplaneId == flight.AirplaneId);
        if (flight.FromCityId != null) flights = flights.Where(x => x.FromCityId == flight.FromCityId);
        if (flight.ToCityId != null) flights = flights.Where(x => x.ToCityId == flight.ToCityId);


        /*var flights = _context.Flights.AsQueryable()
            .Where(x => x.PilotId == 4)
            .Where(x => x.AirlineAirplaneId == 4);*/

        // if (flight.PilotFullName.HasValue) flights = flights.Where(x => x.Pilot.Name == flight.PilotFullName.Value);


        return await flights.Select(p => new FlightIndexModel
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
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
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