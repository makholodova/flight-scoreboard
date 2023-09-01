namespace FlightScoreboard.DateBase;

public class Airplane
{
	public Airplane(int id, string model)
	{
		this.Id = id;
		this.Model = model;
	}

	public int Id { get; set; }
	public string Model { get; set; }

	public virtual List<AirplaneByAirline> AirplaneByAirlines { get; set; }
}