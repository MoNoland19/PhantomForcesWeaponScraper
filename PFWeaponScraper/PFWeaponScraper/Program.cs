using System;
using System.Runtime.InteropServices;
using System.Threading;
using PFWeaponScraper.Config;
using PFWeaponScraper.OCR;
using PFWeaponScraper.Screen;

class Program
{
    static void Main()
    {
        // Fix DPI scaling (CRITICAL for correct capture)
        SetProcessDPIAware();

        Console.WriteLine("OCR TEST HARNESS");
        Console.WriteLine("Focus the GAME WINDOW in 5 seconds...");
        Thread.Sleep(1000);

        var capture = new ScreenCaptureService();
        var ocr = new OcrReader();

        TestRegion(capture, ocr, ScreenRegions.GunName, "Gun Name");
        TestRegion(capture, ocr, ScreenRegions.Optic, "Optic");
        TestRegion(capture, ocr, ScreenRegions.Underbarrel, "Underbarrel");
        TestRegion(capture, ocr, ScreenRegions.Barrel, "Barrel");
        TestRegion(capture, ocr, ScreenRegions.Other, "Other");
        TestRegion(capture, ocr, ScreenRegions.Ammo, "Ammo");

        Console.WriteLine();
        Console.WriteLine("OCR test complete.");
        Console.ReadLine();
    }

    private static void TestRegion(
        ScreenCaptureService capture,
        OcrReader ocr,
        RelativeRegion region,
        string label)
    {
        using var image = capture.CaptureRegion(region);
        string text = ocr.ReadText(image);

        Console.WriteLine($"{label,-12}: [{text}]");
    }

    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();
}