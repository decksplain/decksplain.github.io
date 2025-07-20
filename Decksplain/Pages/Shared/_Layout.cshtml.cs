using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decksplain.Pages.Shared;

public class LayoutModel : PageModel
{
    public Layout Layout { get; set; } = new();
}

public class Layout
{
    public bool IsPrint { get; set; } = false;
    public string? Title { get; set; } = null;
}
