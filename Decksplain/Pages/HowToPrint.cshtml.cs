using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Decksplain.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Pages;

public class HowToPrintModel : LayoutModel
{
    public HowToPrintModel(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
    }
    
    public IActionResult OnGetAsync()
    {
        Layout.Title = "How to print";
        
        return Page();
    }
}
