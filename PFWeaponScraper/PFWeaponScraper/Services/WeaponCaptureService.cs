using PFWeaponScraper.Config;
using PFWeaponScraper.Models;
using PFWeaponScraper.OCR;
using PFWeaponScraper.Screen;

namespace PFWeaponScraper.Services;

public class WeaponCaptureService
{
    private readonly ScreenCaptureService _screen;
    private readonly IScreenTextReader _ocr;
    private readonly WeaponBuilder _builder;

    public WeaponCaptureService(
        ScreenCaptureService screen,
        IScreenTextReader ocr,
        WeaponBuilder builder)
    {
        _screen = screen;
        _ocr = ocr;
        _builder = builder;
    }

    public Weapon CaptureWeapon()
    {
        string gun = Read(ScreenRegions.GunName);
        string optic = Read(ScreenRegions.Optic);
        string barrel = Read(ScreenRegions.Barrel);
        string underbarrel = Read(ScreenRegions.Underbarrel);
        string other = Read(ScreenRegions.Other);
        string ammo = Read(ScreenRegions.Ammo);

        return _builder.Build(
            gun,
            optic,
            barrel,
            underbarrel,
            other,
            ammo
        );
    }

    private string Read(RelativeRegion region)
    {
        using var image = _screen.CaptureRegion(region);
        return _ocr.ReadText(image);
    }
}