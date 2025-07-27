using Decksplain.Features.Card;
using Decksplain.Features.Layout;

namespace Decksplain.Features.Games.Printable;

public class GamesPrintable : LayoutContainer
{
    public required CardDto[] Cards { get; set; }
}
