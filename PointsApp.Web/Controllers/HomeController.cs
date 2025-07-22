using Microsoft.AspNetCore.Mvc;

namespace PointsApp.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}