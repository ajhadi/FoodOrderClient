using FoodOrderClient.Models;
using FoodOrderClient.Services.FoodOrderAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodOrderClient.Pages;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> logger;
    private readonly IFoodOrderAPI foodOrderAPI;

    public LoginModel(ILogger<LoginModel> logger, IFoodOrderAPI foodOrderAPI)
    {
        this.logger = logger;
        this.foodOrderAPI = foodOrderAPI;
    }
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnGet()
    {
        if (Request.Cookies["access_token"] != null)
            return RedirectToPage("./Index");
        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        var result = await foodOrderAPI.GetTokenAsync(Username, Password);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error.Message;
            return Page();
        }
        var cookieOptions = new CookieOptions
        {
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            Expires = DateTime.Now.AddHours(1)
        };
        Response.Cookies.Append("access_token", result.Result.Token, cookieOptions);
        return RedirectToPage("./Index");
    }
}

