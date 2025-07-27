using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.Index;

public class IndexController : Controller
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;

    public IndexController(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult Index()
    {
        var model = new Index
        {
            Layout = new Layout.Layout
            {
                Title = "Home"
            },
            Cards = _gamesRepository.GetGames()
                .Select(game => _cardFactory.CreateFromGame(game))
                .ToArray()
        };
        
        return View("~/Features/Index/Index.cshtml", model);
    }
}
