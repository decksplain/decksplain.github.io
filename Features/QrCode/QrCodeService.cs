using System.Drawing;
using QRCoder;

namespace Decksplain.Features.QrCode;

public class QrCodeService
{
    private const Base64QRCode.ImageType ImgType = Base64QRCode.ImageType.Png;
    
    public string GenerateBase64(string content)
    {
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

        Base64QRCode qrCode = new Base64QRCode(qrCodeData);
        
        return qrCode.GetGraphic(
            pixelsPerModule: 20,
            darkColor: Color.FromArgb(1, 40, 76), 
            lightColor: Color.Transparent, 
            drawQuietZones: false,
            imgType: ImgType
        );
    }
}
