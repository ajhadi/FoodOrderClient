using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodOrderClient.Pages;

public class LogoutModel : PageModel
{

    public LogoutModel()
    {
    }

    public IActionResult OnGet()
    {
        if (Request.Cookies["access_token"] != null)
            Response.Cookies.Delete("access_token");
        return RedirectToPage("./Login");
    }
}

