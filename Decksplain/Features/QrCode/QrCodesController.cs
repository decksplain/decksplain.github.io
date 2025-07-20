using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Decksplain.Features.QrCode;

[Route("api/[controller]")]
[ApiController]
public class QrCodesController : ControllerBase
{
    private readonly QrCodeService _qrCodeService;

    public QrCodesController(QrCodeService qrCodeService)
    {
        _qrCodeService = qrCodeService;
    }

    /// <summary>
    /// <remarks>
    /// <see cref="base64UrlContent"/> must be base64url encoded because there were issues with `encodeURIComponent`.
    /// e.g:
    /// - encodeURIComponent("https://www.regicidegame.com/how-to-play") outputs "https%3A%2F%2Fwww.regicidegame.com%2Fhow-to-play"
    /// - however, wget stored the file as `https_%2F%2Fwww.regicidegame.com%2Fhow-to-play%2F` but linked it with https:%252F%252Fwww.regicidegame.com%252Fhow-to-play%252F
    ///
    /// Also, base64 can encode to `+` and `/`.
    /// </remarks>
    /// </summary>
    /// <param name="base64UrlContent"></param>
    /// <returns></returns>
    [HttpGet("{base64UrlContent}")]
    public IActionResult Get(string base64UrlContent)
    {
        var base64EncodedBytes = Base64Url.DecodeFromChars(base64UrlContent);
        string decoded = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        byte[] imageBytes = Convert.FromBase64String(_qrCodeService.GenerateBase64(decoded));

        return File(imageBytes, $"image/{nameof(Base64QRCode.ImageType.Png).ToLower()}");
    }
}
