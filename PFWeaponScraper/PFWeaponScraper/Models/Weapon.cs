using System.Text.Json.Serialization;
namespace PFWeaponScraper.Models;

public class Weapon : IComparable<Weapon>
{
    public string GunName { get; }
    public IReadOnlyList<string> Attachments { get; }

    [JsonConstructor]
    public Weapon(string GunName, IReadOnlyList<string> Attachments)
    {
        this.GunName = GunName.Trim();
        this.Attachments = Normalize(Attachments);
    }

    public Weapon(
        string gun,
        string optic,
        string barrel,
        string underbarrel,
        string other,
        string ammo)
    {
        GunName = gun.Trim();
        Attachments = Normalize(new[]
        {
            optic, barrel, underbarrel, other, ammo
        });
    }

    private static IReadOnlyList<string> Normalize(IEnumerable<string> attachments)
    {
        return attachments
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Select(a => a.Trim())
            .Where(a => !string.Equals(a, "None", StringComparison.OrdinalIgnoreCase))
            .OrderBy(a => a, StringComparer.OrdinalIgnoreCase)
            .ToList()
            .AsReadOnly();
    }

    public int AttachmentCount => Attachments.Count;

    public int CompareTo(Weapon? other)
    {
        if (other is null) return 1;

        int gunCompare = string.Compare(
            GunName,
            other.GunName,
            StringComparison.OrdinalIgnoreCase);

        if (gunCompare != 0)
            return gunCompare;

        return other.AttachmentCount.CompareTo(AttachmentCount);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Weapon other)
            return false;

        return GunName.Equals(other.GunName, StringComparison.OrdinalIgnoreCase)
            && Attachments.SequenceEqual(
                other.Attachments,
                StringComparer.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        int hash = GunName.ToLowerInvariant().GetHashCode();

        foreach (var att in Attachments)
            hash ^= att.ToLowerInvariant().GetHashCode();

        return hash;
    }
}
