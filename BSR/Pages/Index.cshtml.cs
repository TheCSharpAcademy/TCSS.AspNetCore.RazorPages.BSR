using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSR.Pages;

public class IndexModel : PageModel
{
    public string Message { get; set; }

    public void OnGet()
    {
        Message = "Hello from the backend!";
    }
}
