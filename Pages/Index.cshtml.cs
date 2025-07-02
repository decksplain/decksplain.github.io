using Decksplain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decksplain.Pages;

public class IndexModel : PageModel
{
    private readonly GamesRepository _gamesRepository;
    public required Card[] Cards { get; set; }

    public IndexModel(GamesRepository  gamesRepository)
    {
        _gamesRepository = gamesRepository;
    }
    
    public IActionResult OnGetAsync()
    {
        Cards = _gamesRepository.GetGames()
            .Select(game => new Card(game))
            .ToArray();
        
        return Page();
    }
}
