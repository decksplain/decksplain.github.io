using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decksplain.Pages;

public class IndexModel : PageModel
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;
    public required CardDto[] Cards { get; set; }

    public IndexModel(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult OnGetAsync()
    {
        Cards = _gamesRepository.GetGames()
            .Select(game => _cardFactory.CreateFromGame(game))
            .ToArray();
        
        return Page();
    }
}
