using FlightScoreboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class ErrorController : Controller
{
    public IActionResult Index(ErrorModel error)
    {
        
        return View(error);
    }
}