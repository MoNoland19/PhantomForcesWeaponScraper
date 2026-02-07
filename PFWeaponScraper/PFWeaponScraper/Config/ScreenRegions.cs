namespace PFWeaponScraper.Config;

public static class ScreenRegions
{
    public static readonly RelativeRegion GunName =
        new(0.32f, 0.18f, 0.36f, 0.04f);

    public static readonly RelativeRegion Optic =
        new(0.32f, 0.24f, 0.36f, 0.04f);

    public static readonly RelativeRegion Barrel =
        new(0.32f, 0.30f, 0.36f, 0.04f);

    public static readonly RelativeRegion Underbarrel =
        new(0.32f, 0.36f, 0.36f, 0.04f);

    public static readonly RelativeRegion Other =
        new(0.32f, 0.42f, 0.36f, 0.04f);

    public static readonly RelativeRegion Ammo =
        new(0.32f, 0.48f, 0.36f, 0.04f);
}