using Decksplain.Extensions;
using Decksplain.Features.BaseUrl;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Decksplain.Features.Card;

public class CardFactory
{
    private readonly LinkGenerator _linkGenerator;
    private readonly BaseUrlService _baseUrlService;

    public CardFactory(LinkGenerator linkGenerator, BaseUrlService  baseUrlService)
    {
        _linkGenerator = linkGenerator;
        _baseUrlService = baseUrlService;
    }
    
    public CardDto CreateFromGame(Game.GameModel gameModel)
    {
        string relativeUrl = _linkGenerator.GetPathByAction("Index", "Game", new { title = gameModel.Title.Slugify() })
            ?? throw new Exception("Unable to find game URL");
        string absoluteUrl = _baseUrlService.GetBaseUrl() + relativeUrl;
        byte[] urlBytes = System.Text.Encoding.UTF8.GetBytes(absoluteUrl);
        string base64Url = System.Buffers.Text.Base64Url.EncodeToString(urlBytes);
        
        string[] contentSplit = gameModel.Content.Split("<!--split-->");
        
        CardDto card = new()
        {
            RelativeUrl = relativeUrl,
            QrCodeUrl = $"/api/qrcodes/{base64Url}",
            Title = gameModel.Title,
            Players = gameModel.Players,
            RoundTime = gameModel.RoundTime,
            Description = gameModel.Description,
            FrontContent = contentSplit[0],
            BackContent = contentSplit.Length > 1 ? contentSplit[1] : null
        };
        
        return card;
    }
}
