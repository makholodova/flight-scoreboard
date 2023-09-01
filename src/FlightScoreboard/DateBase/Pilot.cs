namespace FlightScoreboard.DateBase;

public class Pilot
{
	public Pilot(int id, string name, string surName, int age, int airlineId)
	{
		this.Id = id;
		this.Name = name;
		this.SurName = surName;
		this.Age = age;
		this.AirlineId = airlineId;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string SurName { get; set; }
	public int Age { get; set; }
	
	public int AirlineId { get; set; }
	public virtual Airline Airline { get; set; }
	
	public virtual List<Flight> Flights { get; set; }
}