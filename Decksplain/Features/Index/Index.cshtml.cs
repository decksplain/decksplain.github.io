using Decksplain.Features.Card;
using Decksplain.Features.Layout;

namespace Decksplain.Features.Index;

public class Index : LayoutContainer
{
    public required CardDto[] Cards { get; init; }
}
