using System.Drawing;

namespace PFWeaponScraper.OCR;

public interface IScreenTextReader
{
    string ReadText(Bitmap image);
}