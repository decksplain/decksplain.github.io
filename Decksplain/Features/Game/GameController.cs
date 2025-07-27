using Decksplain.Features.Card;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.Game;

public class GameController : Controller
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;

    public GameController(GamesRepository gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult Index(string? title)
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
        
        CardDto cardDto = _cardFactory.CreateFromGame(game);
        
        var model = new Game
        {
            Layout = new Layout.Layout
            {
                Title = $"{cardDto.Title} | Games"
            },
            CardDto = cardDto
        };
        
        return View("~/Features/Game/Game.cshtml", model);
    }
}
