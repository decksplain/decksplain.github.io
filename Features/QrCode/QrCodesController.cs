using System.Web;
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

    [HttpGet("{content}")]
    public IActionResult Get(string content)
    {
        string decoded = HttpUtility.UrlDecode(content);
        byte[] imageBytes = Convert.FromBase64String(_qrCodeService.GenerateBase64(decoded));

        return File(imageBytes, $"image/{nameof(Base64QRCode.ImageType.Png).ToLower()}");
    }
}
