using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Decksplain.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Pages;

public class GamesModel : LayoutModel
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;
    public required CardDto[] Cards { get; set; }

    public GamesModel(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult OnGetAsync()
    {
        Layout.Title = "Games";
        
        Cards = _gamesRepository.GetGames()
            .Select(game => _cardFactory.CreateFromGame(game))
            .ToArray();
        
        return Page();
    }
}
