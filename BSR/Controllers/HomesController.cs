using BSR.Models;
using BSR.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSR.Controllers;

public class HomesController: Controller
{
    private readonly HomeService _homeService;
    public List<Home> Homes { get; private set; }

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
}
