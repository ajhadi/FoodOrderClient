using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodOrderClient.Pages;

public class OrderModel : PageModel
{
    private readonly ILogger<OrderModel> logger;

    public OrderModel(ILogger<OrderModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet()
    {
    }
}

