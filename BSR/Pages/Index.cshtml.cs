using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSR.Pages;

public class IndexModel : PageModel
{
    public List<Home> Homes { get; private set; }
    public decimal ThresholdPrice { get; set; }

    public void OnGet()
    {
        Homes = new List<Home>
        {
            new Home { Price = 300000, Address = "123 Main St", Area = 120 },
            new Home { Price = 450000, Address = "456 Elm St", Area = 150 },
            new Home { Price = 350000, Address = "789 Oak St", Area = 130 },
            new Home { Price = 500000, Address = "101 Pine St", Area = 160 },
            new Home { Price = 275000, Address = "202 Birch St", Area = 115 },
            new Home { Price = 600000, Address = "303 Cedar St", Area = 180 },
            new Home { Price = 320000, Address = "404 Maple St", Area = 140 },
            new Home { Price = 470000, Address = "505 Cherry St", Area = 155 },
            new Home { Price = 380000, Address = "606 Walnut St", Area = 145 },
            new Home { Price = 420000, Address = "707 Ash St", Area = 150 },
            new Home { Price = 310000, Address = "808 Willow St", Area = 135 },
            new Home { Price = 440000, Address = "909 Redwood St", Area = 158 }
        };
        ThresholdPrice = 400000;
    }
}
