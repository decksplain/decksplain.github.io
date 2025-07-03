using Decksplain.Extensions;
using Decksplain.Service;

public class Card
{
    public Card(Game game, string domain)
    {
        Title = game.Title;
        Players = game.Players;
        RoundTime = game.RoundTime;
        
        string[] contentSplit = game.Content.Split("<!--split-->");
        FrontContent = contentSplit[0];
        
        if (contentSplit.Length > 1)
            BackContent = contentSplit[1];

        Url = $"/games/{Title.Slugify()}";

        QrCode = new QrCodeService().Generate(domain + Url, "5rem");
    }

    public string Url { get; set; }

    public string Title { get; set; }

    public string Players { get; set; }

    public string RoundTime { get; set; }

    public string FrontContent { get; set; }
    
    public string BackContent { get; set; }
    
    public string QrCode { get; set; } 
}
