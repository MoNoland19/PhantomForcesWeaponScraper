using System;
using System.IO;
using System.Drawing;
using PFWeaponScraper.Utils;
using Tesseract;

namespace PFWeaponScraper.OCR;

public class OcrReader : IScreenTextReader
{
    private readonly TesseractEngine _engine;

    public OcrReader()
    {
        string baseDir = AppContext.BaseDirectory;
        string tessdataPath = Path.Combine(baseDir, "tessdata");

        if (!Directory.Exists(tessdataPath))
            throw new DirectoryNotFoundException(
                $"tessdata folder not found at: {tessdataPath}");

        _engine = new TesseractEngine(
            tessdataPath,
            "eng",
            EngineMode.Default
        );

        _engine.SetVariable(
            "tessedit_char_whitelist",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_. "
        );
    }

    public string ReadText(Bitmap image)
    {
        using var pix = PixConverter.ToPix(image);
        using var page = _engine.Process(pix);

        return TextNormalizer.Normalize(page.GetText());
    }
}