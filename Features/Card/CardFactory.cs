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
        string url = $"/games/{gameModel.Title.Slugify()}";
        string fullDomain = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        
        string[] contentSplit = gameModel.Content.Split("<!--split-->");
        
        CardDto card = new()
        {
            Title = gameModel.Title,
            Players = gameModel.Players,
            RoundTime = gameModel.RoundTime,
            RelativeUrl = url,
            FrontContent = contentSplit[0],
            BackContent = contentSplit.Length > 1 ? contentSplit[1] : null,
            QrCode = _qrCodeService.Generate(fullDomain + url, "5rem")
        };
        
        return card;
    }
}
