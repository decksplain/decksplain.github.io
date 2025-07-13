using Decksplain.Features.BaseUrl;
using Decksplain.Features.Card;
using Decksplain.Features.Game;
using Decksplain.Features.MarkdownWithFrontMatter;
using Decksplain.Features.QrCode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
