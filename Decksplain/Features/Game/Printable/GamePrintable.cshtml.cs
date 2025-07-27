using Decksplain.Features.Card;
using Decksplain.Features.Layout;

namespace Decksplain.Features.Game.Printable;

public class GamePrintable : LayoutContainer
{
    public required CardDto CardDto { get; set; }
}
