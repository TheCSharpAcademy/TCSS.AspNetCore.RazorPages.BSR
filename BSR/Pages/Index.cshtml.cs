using BSR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSR.Pages;

public class IndexModel : PageModel
{
    private readonly HomeService _homeService;
    public List<Home> Homes { get; private set; }
    public decimal ThresholdPrice { get; set; }

    public IndexModel(HomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult OnGet()
    {
        try
        {
            Homes = _homeService.GetHomes();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error fetching home from the database: {ex.Message}";
        }

        return Page();
    }
}
