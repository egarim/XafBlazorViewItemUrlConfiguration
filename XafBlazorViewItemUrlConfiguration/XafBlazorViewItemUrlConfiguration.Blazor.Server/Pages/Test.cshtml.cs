using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XafBlazorViewItemUrlConfiguration.Blazor.Server.Pages
{
    [AllowAnonymous]
    public class TestPageModel : PageModel
    {
        public string? Message { get; set; }
        
        public void OnGet(string? message = null)
        {
            Message = message ?? "(no message)";
        }
    }
}
