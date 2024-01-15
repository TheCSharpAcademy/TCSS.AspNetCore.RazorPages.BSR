using BSR.Services;
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

    public void OnGet()
    {
        Homes = _homeService.GetHomes(); // Use HomeService to get the homes
        ThresholdPrice = 400000;
    }
}
