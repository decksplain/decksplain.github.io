using Decksplain.Extensions;
using Decksplain.Features.MarkdownWithFrontMatter;

namespace Decksplain.Features.Game;

public class GamesRepository
{
    private readonly MarkdownWithFrontMatterParserService _markdownWithFrontMatterParserService;

    public GamesRepository(MarkdownWithFrontMatterParserService markdownWithFrontMatterParserService)
    {
        _markdownWithFrontMatterParserService = markdownWithFrontMatterParserService;
    }

    public GameModel[] GetGames()
    {
        return Directory.EnumerateFiles("Data/Games/", "*.md")
            .Select(File.ReadAllText)
            .Select(markdownWithFrontMatter
                => _markdownWithFrontMatterParserService.Read<GameModel>(markdownWithFrontMatter)
            )
            .Where(game => game is not null)
            .ToArray()!;
    }

    public GameModel? GetGame(string title)
    {
        return GetGames()
            .FirstOrDefault(game => string.Equals(game.Title.Slugify(), title, StringComparison.OrdinalIgnoreCase));
    }
}
