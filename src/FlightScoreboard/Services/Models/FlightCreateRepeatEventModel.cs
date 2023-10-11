namespace FlightScoreboard.Services.Models;

public class FlightCreateRepeatEventModel
{
    public DateTime StartDay { get; set; }
    public DateTime FinishDay { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan DurationTime { get; set; }
    public int FromCityId { get; set; }
    public int ToCityId { get; set; }
    public int PilotId { get; set; }
    public int AirlineId { get; set; }
    public int AirlineAirplaneId { get; set; }
}