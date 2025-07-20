using System.Diagnostics;
using Decksplain.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : LayoutModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Layout.Title = "Error";
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
