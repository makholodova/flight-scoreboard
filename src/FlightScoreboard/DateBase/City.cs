namespace FlightScoreboard.DateBase;

public class City
{
	public City(int id, string name)
	{
		this.Id = id;
		this.Name = name;
	}

	public int Id { get; set; }
	public string Name { get; set; }

	public virtual List<Flight> FromFlights { get; set; }
	public virtual List<Flight> ToFlights { get; set; }
}