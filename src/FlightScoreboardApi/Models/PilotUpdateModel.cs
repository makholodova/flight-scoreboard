namespace FlightScoreboardApi.Models;

public class PilotUpdateModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string SurName { get; set; }
	public int Age { get; set; }
	public int AirlineId { get; set; }
}