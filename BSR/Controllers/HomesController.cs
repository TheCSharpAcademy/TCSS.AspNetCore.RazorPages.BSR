using BSR.Models;
using BSR.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSR.Controllers;

public class HomesController: Controller
{
    private readonly HomeService _homeService;

    public HomesController(HomeService homeService)
    {
        _homeService = homeService;
    }
    public IActionResult Index()
    {
        var homesViewModel = new HomesViewModel();

        try
        {
            homesViewModel.Homes = _homeService.GetHomes();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error fetching home from the database: {ex.Message}";
        }

        return View(homesViewModel);
    }

    [HttpGet]
    public IActionResult AddHomeView()
    {
        return View(); 
    }

    [HttpPost]
    public IActionResult AddHome(Home newHome)
    {
        if (!ModelState.IsValid)
        {
            return View("AddHomeView", newHome); 
        }

        try
        {
            _homeService.AddHome(newHome);
            TempData["SuccessMessage"] = "Home added successfully!";
            return RedirectToAction("Index", "Homes"); 
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error adding home: {ex.Message}";
            return View("AddHomeView", newHome); 
        }
    }
}
