using System;
using PFWeaponScraper.Input;
using PFWeaponScraper.OCR;
using PFWeaponScraper.Screen;
using PFWeaponScraper.Services;
using PFWeaponScraper.Storage;

namespace PFWeaponScraper;

internal static class Program
{
    static void Main()
    {
        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "OneDrive",
            "Desktop",
            "weapons.json"
        );

        var repository = new WeaponRepository(path);
        var builder = new WeaponBuilder();
        var screen = new ScreenCaptureService();
        var ocr = new OcrReader();

        var captureService = new WeaponCaptureService(
            screen,
            ocr,
            builder,
            repository
        );

        var listener = new HotkeyListener();

        listener.OnLPressed += () =>
        {
            var weapon = captureService.CaptureWeapon();

            Console.WriteLine("=== WEAPON TO ADD ===");
            Console.WriteLine($"Gun: '{weapon.GunName}'");
            Console.WriteLine($"Attachments ({weapon.AttachmentCount}):");
            foreach (var a in weapon.Attachments)
                Console.WriteLine($"  - {a}");

            bool added = repository.AddWeapon(weapon);
            repository.Save();

            Console.WriteLine(
                added
                    ? "Weapon added and saved"
                    : "Duplicate weapon — already exists"
            );
            Console.WriteLine();
        };

        listener.Start();

        Console.WriteLine("PFWeaponScraper running");
        Console.WriteLine("Press L to capture weapon");
        Console.WriteLine("Press ESC to exit");

        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
    }
}