using YamlDotNet.Serialization;

public class Card
{
    public Card(Game game)
    {
        Title = game.Title;
        Players = game.Players;
        RoundTime = game.RoundTime;
        
        string[] contentSplit = game.Content.Split("<!--split-->");
        FrontContent = contentSplit[0];
        
        if (contentSplit.Length > 1)
            BackContent = contentSplit[1];
    }
    
    public string Title { get; set; }

    public string Players { get; set; }

    public string RoundTime { get; set; }

    public string FrontContent { get; set; }
    
    public string BackContent { get; set; }
}
