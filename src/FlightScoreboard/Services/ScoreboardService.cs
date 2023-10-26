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

        var scoreboardDeparture = await flights.Select(p => new ScoreboardDepartureIndexModel
        {
            ArrivalTime = p.ArrivalTime,
            DepartureTime = p.DepartureTime,
            ToCity = p.ToCity.Name,
            NumberOfFlight = p.NumberOfFlight,
            Gate = p.FromGate,
            Terminal = p.FromGate,
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplaneId,
            ActualDepartureTime = p.ActualDepartureTime,
            ActualArrivalTime = p.ActualArrivalTime,
            CheckInStartTime = p.CheckInStartTime,
            CheckInEndTime = p.CheckInEndTime,
            BoardingStartTime = p.BoardingStartTime,
            BoardingEndTime = p.BoardingEndTime
        }).ToListAsync();
        foreach (var scoreboard in scoreboardDeparture) scoreboard.StatusMessage = InstallDepartureStatus(scoreboard);
        return scoreboardDeparture;
    }


    public async Task<List<ScoreboardArrivalIndexModel>> GetArrivalFlightsAsync(int? cityId, DateTime? dateTime)
    {
        var flights = _context.Flights.AsQueryable();
        if (cityId != null) flights = flights.Where(p => p.FromCityId == cityId);
        if (dateTime != null) flights = flights.Where(p => p.ArrivalTime.Date == dateTime);


        var scoreboardArrival = await flights.Select(p => new ScoreboardArrivalIndexModel
        {
            AirlineName = p.Airline.Name,
            AirlineId = p.AirlineId,
            NumberOfFlight = p.NumberOfFlight,
            FromCity = p.FromCity.Name,
            ArrivalTime = p.ArrivalTime,
            DepartureTime = p.DepartureTime,
            AirplaneModel = p.AirlineAirplane.Airplane.Model,
            AirplaneId = p.AirlineAirplane.AirplaneId,
            Terminal = p.ToTerminal,
            Gate = p.ToGate,
            ActualDepartureTime = p.ActualDepartureTime,
            ActualArrivalTime = p.ActualArrivalTime
            /*CheckInStartTime = p.CheckInStartTime,
            CheckInEndTime = p.CheckInEndTime,
            BoardingStartTime = p.BoardingStartTime,
            BoardingEndTime = p.BoardingEndTime*/
        }).ToListAsync();

        foreach (var scoreboard in scoreboardArrival) scoreboard.StatusMessage = InstallArrivalStatus(scoreboard);

        return scoreboardArrival;
    }

    private string InstallArrivalStatus(ScoreboardArrivalIndexModel scoreboardArrival)
    {
        if (scoreboardArrival.ActualArrivalTime != null)
            return "Прибыл  " + scoreboardArrival.ActualArrivalTime;

        if (scoreboardArrival.ActualDepartureTime != null)
            return "Вылетел  " + scoreboardArrival.ActualDepartureTime;

        if (scoreboardArrival.DepartureTime < scoreboardArrival.ActualDepartureTime)
            return "Задерживается, самолет вылетел " + scoreboardArrival.ActualDepartureTime;
        if (scoreboardArrival.DepartureTime < DateTime.Now)
            return "Задерживается, запланированное время вылета " + scoreboardArrival.DepartureTime;

        return "";
    }

    private string InstallDepartureStatus(ScoreboardDepartureIndexModel scoreboardDeparture)
    {
        if (scoreboardDeparture.ActualArrivalTime != null)
            return "Прибыл  " + scoreboardDeparture.ActualArrivalTime;

        if (scoreboardDeparture.ActualDepartureTime != null)
            return "Вылетел  " + scoreboardDeparture.ActualDepartureTime;
        
        if (scoreboardDeparture.BoardingStartTime != null)
            return "Посадка окончена ";

        if (scoreboardDeparture.BoardingStartTime != null)
            return "Идет посадка ";
        
        if (scoreboardDeparture.CheckInEndTime != null)
            return "Регистрация окончена ";
        
        if (scoreboardDeparture.CheckInStartTime != null)
            return "Идет регистрация до  " +
                   scoreboardDeparture.CheckInStartTime.Value.TimeOfDay.Add(new TimeSpan(2, 0, 0));
        
        if (scoreboardDeparture.DepartureTime < scoreboardDeparture.ActualDepartureTime)
            return "Задерживается, самолет вылетел " + scoreboardDeparture.ActualDepartureTime;
        
        if (scoreboardDeparture.DepartureTime < DateTime.Now)
            return "Задерживается, запланированное время вылета " + scoreboardDeparture.DepartureTime;

        return "Начало регистрации " + scoreboardDeparture.DepartureTime.TimeOfDay.Add(new TimeSpan(-2, 0, 0));
    }
}