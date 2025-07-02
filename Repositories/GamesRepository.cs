using Decksplain.Extensions;

namespace Decksplain.Repositories;

public class GamesRepository
{
    public Game[] GetGames()
    {
        return Directory.EnumerateFiles("Data/Games/", "*.md")
            .Select(File.ReadAllText)
            .Select(markdownWithFrontMatter => markdownWithFrontMatter.ReadMarkdownWithFrontMatter<Game>())
            .Where(game => game is not null)
            .ToArray()!;
    }

    public Game? GetGame(string title)
    {
        return GetGames()
            .FirstOrDefault(game => string.Equals(game.Title.Slugify(), title, StringComparison.OrdinalIgnoreCase));
    }
}