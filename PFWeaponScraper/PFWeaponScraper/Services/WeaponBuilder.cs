using PFWeaponScraper.Models;
using PFWeaponScraper.Utils;

namespace PFWeaponScraper.Services;

public class WeaponBuilder
{
    public Weapon Build(
        string? gun,
        string? optic,
        string? barrel,
        string? underbarrel,
        string? other,
        string? ammo)
    {
        return new Weapon(
            TextNormalizer.NormalizeGunName(gun),
            TextNormalizer.NormalizeAttachment(optic),
            TextNormalizer.NormalizeAttachment(barrel),
            TextNormalizer.NormalizeAttachment(underbarrel),
            TextNormalizer.NormalizeAttachment(other),
            TextNormalizer.NormalizeAttachment(ammo)
        );
    }
}