namespace Decksplain.Features.BaseUrl;

public class BaseUrlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseUrlService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetBaseUrl()
    {
        return Environment.GetEnvironmentVariable("BASE_URL")
               ?? $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
    }
}
