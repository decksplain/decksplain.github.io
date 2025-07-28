using Decksplain.Features.BaseUrl;
using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Decksplain.Features.MarkdownWithFrontMatter;
using Decksplain.Features.QrCode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});
builder.Services.AddRouting(options =>
{
    options.AppendTrailingSlash = true;
    options.LowercaseUrls = true;
});
builder.Services.AddHttpContextAccessor();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

builder.Services.AddScoped<MarkdownWithFrontMatterParserService>();
builder.Services.AddScoped<QrCodeService>();
builder.Services.AddScoped<CardFactory>();
builder.Services.AddScoped<GamesRepository>();
builder.Services.AddScoped<BaseUrlService>();

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllerRoute(
    name: "gamesPrintable",
    pattern: "games/printable",
    defaults: new { controller = "GamesPrintable", action = "Index" }
);
app.MapControllerRoute(
    name: "game",
    pattern: "games/{title}",
    defaults: new { controller = "Game", action = "Index" }
);
app.MapControllerRoute(
    name: "gamePrintable",
    pattern: "games/{title}/printable",
    defaults: new { controller = "GamePrintable", action = "Index" }
);
app.MapControllerRoute(
    name: "games",
    pattern: "games",
    defaults: new { controller = "Games", action = "Index" }
);
app.MapControllerRoute(
    name: "howToPrint",
    pattern: "how-to-print",
    defaults: new { controller = "HowToPrint", action = "Index" }
);
app.MapControllerRoute(
    name: "default",
    pattern: "/",
    defaults: new { controller = "Index", action = "Index" }
);

app.Run();
