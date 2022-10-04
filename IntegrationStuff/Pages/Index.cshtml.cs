using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntegrationStuff.Pages;

public class IndexModel : PageModel
{
    [BindProperty] public string Input { get; set; }

    public void OnPost() => Console.WriteLine($"Posted {Input}");
}