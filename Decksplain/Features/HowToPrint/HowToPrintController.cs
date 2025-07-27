using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.HowToPrint;

public class HowToPrintController : Controller
{
    public IActionResult Index()
    {
        var model = new HowToPrint
        {
            Layout = new Layout.Layout
            {
                Title = "How to print"
            }
        };
        
        return View("~/Features/HowToPrint/HowToPrint.cshtml", model);
    }
}
