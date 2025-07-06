namespace Decksplain.Features.Card;

public record CardDto
{
    public required string RelativeUrl { get; init; }

    public required string Title { get; init; }

    public required string Players { get; init; }

    public required string RoundTime { get; init; }

    public required string FrontContent { get; init; }
    
    public required string? BackContent { get; init; }
    
    public required string QrCode { get; init; }
}
