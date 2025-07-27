using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.Games;

public class GamesController : Controller
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;

    public GamesController(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult Index()
    {
        var model = new Game.Games
        {
            Layout = new Layout.Layout
            {
                Title = "Games"
            },
            Cards = _gamesRepository.GetGames()
                .Select(game => _cardFactory.CreateFromGame(game))
                .ToArray()
        };
        
        return View("~/Features/Games/Games.cshtml", model);
    }
}
