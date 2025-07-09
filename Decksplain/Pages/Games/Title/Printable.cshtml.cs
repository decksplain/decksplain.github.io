using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Decksplain.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Pages.Games.Title;

public class PrintableModel : LayoutModel
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;
    public required CardDto CardDto { get; set; }

    public PrintableModel(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
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

        CardDto = _cardFactory.CreateFromGame(game);
        
        return Page();
    }
}
