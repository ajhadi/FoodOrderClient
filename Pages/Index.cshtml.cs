using FoodOrderClient.Models;
using FoodOrderClient.Services.FoodOrderAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodOrderClient.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly IHttpContextAccessor accessor;
    private readonly IFoodOrderAPI foodOrderAPI;

    public IndexModel(ILogger<IndexModel> logger,
    IHttpContextAccessor accessor,
    IFoodOrderAPI foodOrderAPI)
    {
        this.logger = logger;
        this.accessor = accessor;
        this.foodOrderAPI = foodOrderAPI;
    }
    public List<TableDTO> Tables { get; set; }
    public List<ItemDTO> Items { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (Request.Cookies["access_token"] == null)
            return RedirectToPage("./Login");
        var getTables = await foodOrderAPI.GetTablesAsync(Request.Cookies["access_token"]);
        if (!getTables.IsSuccess)
        {
            if (getTables.Error.HttpStatusCode.ToString().StartsWith("4"))
            {
                TempData["ErrorMessage"] = "Please re-login";
            return RedirectToPage("./Login");
            }
            TempData["ErrorMessage"] = $"{getTables.Error.Message} {getTables.Error.Message}";
            return RedirectToPage("./Error");
        }
        Tables = getTables.Result;

        var getItems = await foodOrderAPI.GetItemsAsync(Request.Cookies["access_token"]);
        if (!getItems.IsSuccess)
        {
            if (getItems.Error.HttpStatusCode.ToString().StartsWith("4"))
            {
                TempData["ErrorMessage"] = "Please re-login";
            return RedirectToPage("./Login");
            }
            TempData["ErrorMessage"] = $"{getItems.Error.Message} {getItems.Error.Message}";
            return RedirectToPage("./Error");
        }
        Items = getItems.Result;
        return Page();
    }
}
