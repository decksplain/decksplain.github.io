using Decksplain.Features.Card;
using Microsoft.AspNetCore.Mvc;

namespace Decksplain.Features.Game.Printable;

public class GamePrintableController : Controller
{
    private readonly GamesRepository _gamesRepository;
    private readonly CardFactory _cardFactory;

    public GamePrintableController(GamesRepository  gamesRepository, CardFactory cardFactory)
    {
        _gamesRepository = gamesRepository;
        _cardFactory = cardFactory;
    }
    
    public IActionResult Index(string title)
    {
        var game = _gamesRepository.GetGame(title);
        
        if (game == null)
        {
            return NotFound();
        }

        CardDto cardDto = _cardFactory.CreateFromGame(game);
        
        var model = new GamePrintable
        {
            Layout = new Layout.Layout
            {
                Title = $"Printable | {cardDto.Title} | Games",
                IsPrint = true
            },
            CardDto = cardDto
        };
        
        return View("~/Features/Game/Printable/GamePrintable.cshtml", model);
    }
}
