using Decksplain.Extensions;
using Decksplain.Features.QrCode;

namespace Decksplain.Features.Card;

public class CardDto
{
    public string Url { get; set; }

    public string Title { get; set; }

    public string Players { get; set; }

    public string RoundTime { get; set; }

    public string FrontContent { get; set; }
    
    public string BackContent { get; set; }
    
    public string QrCode { get; set; }
}
