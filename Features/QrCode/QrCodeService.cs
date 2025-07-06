using System.Drawing;
using QRCoder;

namespace Decksplain.Features.QrCode;

public class QrCodeService
{
    public string Generate(string input, string widthHeight)
    {
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);

        const Base64QRCode.ImageType imgType = Base64QRCode.ImageType.Png;
        Base64QRCode qrCode = new Base64QRCode(qrCodeData);
        string qrCodeImageAsBase64 = qrCode.GetGraphic(
            pixelsPerModule: 20,
            darkColor: Color.FromArgb(1, 40, 76), 
            lightColor: Color.Transparent, 
            drawQuietZones: false,
            imgType: imgType
        );
        
        return  $"""
                 <img
                    class="qr"
                    alt="Embedded QR Code"
                    src="data:image/{imgType.ToString().ToLower()};base64,{qrCodeImageAsBase64}"
                    style="width: {widthHeight}"
                 />
                 """;
    }
}
