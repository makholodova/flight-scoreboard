using System.Linq;
using FlightScoreboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class ErrorController : Controller
{
	public IActionResult Index(ErrorModel error)
	{
		//RouteDataString: "key1=value1&key2=value2"
		error.RouteData = error.RouteDataString.Split('&') //rows: ["key1=value1", "key2=value2"]
			.Select(x => x.Split("=")) //coll: [[key1,value1], [key2,value2]]
			.ToDictionary(x => x[0], x => x[1]); //x: [key1,value1] , [key2,value2]

		return View(error);
	}
}