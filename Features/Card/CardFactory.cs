using Decksplain.Extensions;
using Decksplain.Features.QrCode;

namespace Decksplain.Features.Card;

public class CardFactory
{
    private readonly QrCodeService _qrCodeService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CardFactory(QrCodeService qrCodeService, IHttpContextAccessor httpContextAccessor)
    {
        _qrCodeService = qrCodeService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public CardDto CreateFromGame(Game.GameModel gameModel)
    {
        CardDto card = new()
        {
            Title = gameModel.Title,
            Players = gameModel.Players,
            RoundTime = gameModel.RoundTime,
            Url = $"/games/{gameModel.Title.Slugify()}"
        };
        
        string fullDomain = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

        card.QrCode = _qrCodeService.Generate(fullDomain + card.Url, "5rem");
        
        string[] contentSplit = gameModel.Content.Split("<!--split-->");
        card.FrontContent = contentSplit[0];
        
        if (contentSplit.Length > 1)
            card.BackContent = contentSplit[1];

        return card;
    }
}
