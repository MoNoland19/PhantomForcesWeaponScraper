using System;
using System.IO;
using System.Runtime.InteropServices;
using PFWeaponScraper.Input;
using PFWeaponScraper.OCR;
using PFWeaponScraper.Screen;
using PFWeaponScraper.Services;
using PFWeaponScraper.Storage;

class Program
{
    static void Main()
    {
        SetProcessDPIAware();

        // OneDrive Desktop path
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string dataDir = Path.Combine(desktopPath, "PFWeaponScraper");

        Directory.CreateDirectory(dataDir);

        string filePath = Path.Combine(dataDir, "PFLoadouts.json");

        var captureService = new WeaponCaptureService(
            new ScreenCaptureService(),
            new OcrReader(),
            new WeaponBuilder()
        );

        var repository = new WeaponRepository(filePath);

        using var hotkey = new HotkeyListener(() =>
        {
            try
            {
                var weapon = captureService.CaptureWeapon();
                repository.AddWeapon(weapon);
                repository.Save();

                Console.WriteLine($"Captured: {weapon.GunName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Capture failed: {ex.Message}");
            }
        });

        Console.WriteLine("PFWeaponScraper running");
        Console.WriteLine("Press Alt + L to capture loadout");
        Console.WriteLine("Press ENTER to exit");

        Console.ReadLine();
    }

    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();
}