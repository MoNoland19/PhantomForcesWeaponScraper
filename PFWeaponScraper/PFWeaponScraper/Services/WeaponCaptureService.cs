using PFWeaponScraper.Config;
using PFWeaponScraper.Models;
using PFWeaponScraper.OCR;
using PFWeaponScraper.Screen;
using PFWeaponScraper.Storage;

namespace PFWeaponScraper.Services;

public class WeaponCaptureService
{
    private readonly ScreenCaptureService _screen;
    private readonly IScreenTextReader _ocr;
    private readonly WeaponBuilder _builder;
    private readonly WeaponRepository _repository;

    public WeaponCaptureService(
        ScreenCaptureService screen,
        IScreenTextReader ocr,
        WeaponBuilder builder,
        WeaponRepository repository)
    {
        _screen = screen;
        _ocr = ocr;
        _builder = builder;
        _repository = repository;
    }

    public void CaptureAndStoreWeapon()
    {
        var weapon = CaptureWeapon();
        _repository.AddWeapon(weapon);
        _repository.Save();
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