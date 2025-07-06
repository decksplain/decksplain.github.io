using Decksplain.Extensions;
using Decksplain.Features.BaseUrl;

namespace Decksplain.Features.Card;

public class CardFactory
{
    private readonly BaseUrlService _baseUrlService;

    public CardFactory(BaseUrlService  baseUrlService)
    {
        _baseUrlService = baseUrlService;
    }
    
    public CardDto CreateFromGame(Game.GameModel gameModel)
    {
        string url = $"{_baseUrlService.GetBaseUrl()}/games/{gameModel.Title.Slugify()}";
        byte[] urlBytes = System.Text.Encoding.UTF8.GetBytes(url);
        string base64Url = Convert.ToBase64String(urlBytes);
        
        string[] contentSplit = gameModel.Content.Split("<!--split-->");
        
        CardDto card = new()
        {
            RelativeUrl = url,
            QrCodeUrl = $"/api/qrcodes/{base64Url}",
            Title = gameModel.Title,
            Players = gameModel.Players,
            RoundTime = gameModel.RoundTime,
            FrontContent = contentSplit[0],
            BackContent = contentSplit.Length > 1 ? contentSplit[1] : null
        };
        
        return card;
    }
}
