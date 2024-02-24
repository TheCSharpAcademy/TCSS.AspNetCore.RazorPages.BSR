using BSR.Models;
using BSR.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BSR.Controllers;

public class HomesController : Controller
{
    private readonly ILogger<HomesController> _logger; 
    private readonly HomeService _homeService;
    private readonly AddressService _addressService;

    public HomesController(ILogger<HomesController> logger, HomeService homeService, AddressService addressService)
    {
        _logger = logger; 
        _homeService = homeService;
        _addressService = addressService;
    }

    public async Task<IActionResult> GetCities(string state)
    {
        try
        {
            var cities = await _addressService.GetCitiesInState(state);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred in GetCities action for state: {state}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> AddHomeView()
    {
        try
        {
            var statesResult = await _addressService.GetAmericanStates();

            var addHomeViewModel = new AddHomeViewModel
            {
                States = statesResult,
                Cities = new List<string>()
            };

            return View(addHomeViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading add home view");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    public async Task<IActionResult> Index(int? minPrice, int? maxPrice, int? minArea, int? maxArea, int pageNumber = 1, int pageSize = 10)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var homesViewModel = new HomesViewModel();

        try
        {
            var homes = _homeService.GetHomes();

            // Apply the price range filter if values are provided
            if (minPrice.HasValue)
            {
                homes = homes.Where(h => h.Price >= minPrice.Value).ToList();
            }
            if (maxPrice.HasValue)
            {
                homes = homes.Where(h => h.Price <= maxPrice.Value).ToList();
            }

            // Apply the area range filter if values are provided
            if (minArea.HasValue)
            {
                homes = homes.Where(h => h.Area >= minArea.Value).ToList();
            }
            if (maxArea.HasValue)
            {
                homes = homes.Where(h => h.Area <= maxArea.Value).ToList();
            }

            int totalItems = homes.Count();
            homes = homes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            homesViewModel.Homes = homes;
            homesViewModel.PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNumber,
                ItemsPerPage = pageSize,
                TotalItems = totalItems
            };

            ViewBag.HomesCount = totalItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching homes from the database: {ex.Message}");
            TempData["ErrorMessage"] = $"Error fetching homes from the database: {ex.Message}";
        }

        // Pass the filter values back to the view to retain them
        homesViewModel.MinPrice = minPrice;
        homesViewModel.MaxPrice = maxPrice;
        homesViewModel.MinArea = minArea;
        homesViewModel.MaxArea = maxArea;

        stopwatch.Stop();

        ViewBag.LoadTestTime = stopwatch.Elapsed.TotalSeconds.ToString("F4"); // Return the elapsed time in milliseconds

        _logger.LogWarning("Hipotetical warning");

        return View(homesViewModel);
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
            _logger.LogError(ex, $"Error fetching homes from the database: {ex.Message}");
            TempData["ErrorMessage"] = $"Error adding home: {ex.Message}";
            return View("AddHomeView", newHome);
        }
    }

    [HttpGet]
    public IActionResult HomeDetailView(int id)
    {
        try
        {
            var home = _homeService.GetHomeById(id);
            return View(home);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching home details for ID: {id}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public IActionResult Update(Home updatedHome)
    {
        if (!ModelState.IsValid)
        {
            return View("HomeDetailView", updatedHome);
        }

        try
        {
            _homeService.UpdateHome(updatedHome);
            TempData["SuccessMessage"] = "Home updated successfully!";
            return RedirectToAction("Index", "Homes");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching homes from the database: {ex.Message}");
            TempData["ErrorMessage"] = $"Error updating home: {ex.Message}";
            return View("HomeDetailView", updatedHome);
        }
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _homeService.DeleteHome(id);
            TempData["SuccessMessage"] = "Home deleted successfully!";
            return new OkResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting home with ID: {id}");

            TempData["ErrorMessage"] = $"Error deleting home: {ex.Message}";
            return BadRequest(new { message = ex.Message });
        }
    }
}
