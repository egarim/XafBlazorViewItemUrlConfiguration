using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XafBlazorViewItemUrlConfiguration.Blazor.Server.Pages
{
    public class ChatPageModel : PageModel
    {
        public string? Message { get; set; }
        
        public void OnGet(string? message = null)
        {
            Message = message;
        }
    }
}
