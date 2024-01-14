namespace FlightScoreboard.Models;

public class ErrorModel
{
	public string ErrorMessage { get; set; }
	public string ActionName { get; set; }
	public string ControllerName { get; set; }
	public Dictionary<string, string> RouteData { get; set; }
	public string RouteDataString { get; set; }
}