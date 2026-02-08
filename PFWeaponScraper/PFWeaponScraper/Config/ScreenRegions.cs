namespace PFWeaponScraper.Config;

public static class ScreenRegions
{
    // Window size used for calibration: 2560x1440
    // Adjusted per requested pixel changes

    public static readonly RelativeRegion GunName =
        new(0.453f, 0.708f, 0.094f, 0.021f);

    // Attachments: 5% taller, centered vertically
    // Optic moved DOWN 2px, Ammo moved UP 2px

    public static readonly RelativeRegion Optic =
        new(0.586f, 0.748f, 0.051f, 0.011f);

    public static readonly RelativeRegion Underbarrel =
        new(0.586f, 0.764f, 0.051f, 0.011f);

    public static readonly RelativeRegion Barrel =
        new(0.586f, 0.781f, 0.051f, 0.011f);

    public static readonly RelativeRegion Other =
        new(0.586f, 0.798f, 0.051f, 0.011f);

    public static readonly RelativeRegion Ammo =
        new(0.586f, 0.814f, 0.051f, 0.011f);
}