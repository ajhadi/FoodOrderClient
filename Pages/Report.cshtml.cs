using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodOrderClient.Pages;

public class ReportModel : PageModel
{
    private readonly ILogger<ReportModel> logger;

    public ReportModel(ILogger<ReportModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet()
    {
    }
}

