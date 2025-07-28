using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.Games.Printable;

public class GamesPrintableController : Controller
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;

    public GamesPrintableController(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult Index()
    {
        var model = new GamesPrintable
        {
            Layout = new Layout.Layout
            {
                Title = "Printable | Games",
                IsPrint = true
            },
            Cards = _gamesRepository.GetGames()
                .Select(game => _cardFactory.CreateFromGame(game))
                .ToArray()
        };
       
        return View("~/Features/Games/Printable/GamesPrintable.cshtml", model);
    }
}
