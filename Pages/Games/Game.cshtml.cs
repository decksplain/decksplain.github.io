using Decksplain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decksplain.Pages.Games;

public class GameModel : PageModel
{
    private readonly GamesRepository _gamesRepository;
    
    public required Card Card { get; set; }

    public GameModel(GamesRepository gamesRepository)
    {
        _gamesRepository = gamesRepository;
    }
    
    public IActionResult OnGetAsync(string? title)
    {
        if (title == null)
        {
            return NotFound();
        }

        var game = _gamesRepository.GetGame(title);
        
        if (game == null)
        {
            return NotFound();
        }

        Card = new Card(game);
        
        return Page();
    }
}
