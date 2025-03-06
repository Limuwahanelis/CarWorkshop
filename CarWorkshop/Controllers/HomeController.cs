using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarWorkshop.Models;

namespace CarWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult NoAccess()
    {
        return View();
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        About ab = new About()
        {
            Description = "About page",
            Title = "Tile page about",
            Tags = new List<string>() { "Web", "Car" }
        };
        return View(ab);
    }
    public IActionResult Privacy()
    {

        List<Person> persons = new List<Person>()
        {
            new Person()
            {
                Firstname = "Jakub",
                Lastname ="Kozera"
            },
            new Person()
            {
                Firstname="Tomek",
                Lastname="Nowak"
            }
        };

        return View(persons);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
