using Decksplain.Features.Card;

namespace Decksplain.Features.Game;

public class Games : Layout.LayoutContainer
{
    public required CardDto[] Cards { get; set; }
}
