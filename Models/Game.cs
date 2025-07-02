using YamlDotNet.Serialization;

public interface IContent
{
    string Content { get; set; }
}

public class Game : IContent
{
    [YamlMember(Alias = "title")]
    public required string Title { get; set; }

    [YamlMember(Alias = "players")]
    public required string Players { get; set; }

    [YamlMember(Alias = "round-time")]
    public required string RoundTime { get; set; }

    public required string Content { get; set; }
}
