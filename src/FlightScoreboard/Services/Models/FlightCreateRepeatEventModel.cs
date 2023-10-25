namespace FlightScoreboard.Services.Models;

public class FlightCreateRepeatEventModel
{
    public DateTime StartDay { get; set; }
    public DateTime FinishDay { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan DurationTime { get; set; }
    
    public DateTime ActualDepartureTime { get; set; }
    public DateTime ActualArrivalTime { get; set; }
    public DateTime CheckInStartTime { get; set; }
    public DateTime CheckInEndTime { get; set; }
    public DateTime BoardingStartTime { get; set; }
    public DateTime BoardingEndTime { get; set; }
    
    public int FromCityId { get; set; }
    public int ToCityId { get; set; }
    public int PilotId { get; set; }
    public int AirlineId { get; set; }
    public int AirlineAirplaneId { get; set; }
    
    public string  NumberOfFlight { get; set; }
    public string  ToGate { get; set; }
    public string  ToTerminal { get; set; }
    public string  FromGate { get; set; }
    public string  FromTerminal { get; set; }
}