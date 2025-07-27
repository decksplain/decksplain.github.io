namespace Decksplain.Features.Layout;

public class LayoutContainer
{
    public required Layout Layout { get; set; }
}

public class Layout
{
    public bool IsPrint { get; set; } = false;
    public string? Title { get; set; } = null;
}
