using BSR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSR.Pages;

public class HomeDetailModel : PageModel
{
    private readonly HomeService _homeService;

    public HomeDetailModel(HomeService homeService)
    {
        _homeService = homeService;
    }

    [BindProperty]
    public Home Home { get; set; }
    public IActionResult OnGet(int id)
    {
        Home = GetHomeById(id);

        return Page();
    }

    private Home GetHomeById(int id)
    {
        return _homeService.GetHomeById(id);
    }

    public IActionResult OnPostUpdate()
    {
        _homeService.UpdateHome(Home);

        return RedirectToPage("/Index");
    }

    public IActionResult OnPostDelete(int id)
    {
        _homeService.DeleteHome(id);

        return new OkResult();
    }
}
