using Decksplain.Features.Card;
using Decksplain.Features.Layout;

namespace Decksplain.Features.Game;

public class Game : LayoutContainer
{
    public required CardDto CardDto { get; set; }
}
