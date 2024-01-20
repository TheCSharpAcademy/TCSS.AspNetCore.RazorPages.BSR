using BSR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSR.Pages;

public class AddHomeModel : PageModel
{
    private readonly HomeService _homeService;

    public AddHomeModel(HomeService homeService)
    {
        _homeService = homeService;
    }

    [BindProperty]
    public Home NewHome { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _homeService.AddHome(NewHome);
            return RedirectToPage("Index");
        }

        return Page();
    }
}
