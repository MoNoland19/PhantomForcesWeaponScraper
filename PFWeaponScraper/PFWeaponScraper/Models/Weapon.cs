using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PFWeaponScraper.Models;

public class Weapon : IComparable<Weapon>
{
    public string GunName { get; }
    public IReadOnlyList<string> Attachments { get; }

    // MAIN constructor (used by your app logic)
    public Weapon(
        string gun,
        string optic,
        string barrel,
        string underbarrel,
        string other,
        string ammo)
    {
        GunName = gun.Trim();

        Attachments = new List<string>
            {
                optic,
                barrel,
                underbarrel,
                other,
                ammo
            }
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Select(a => a.Trim())
            .Where(a => !string.Equals(a, "None", StringComparison.OrdinalIgnoreCase))
            .ToList()
            .AsReadOnly();
    }

    // JSON constructor (USED BY System.Text.Json)
    [JsonConstructor]
    public Weapon(string GunName, IReadOnlyList<string> Attachments)
    {
        this.GunName = GunName.Trim();

        this.Attachments = Attachments
            .Where(a => !string.IsNullOrWhiteSpace(a))
            .Select(a => a.Trim())
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
            StringComparison.OrdinalIgnoreCase
        );

        if (gunCompare != 0)
            return gunCompare;

        return other.AttachmentCount.CompareTo(AttachmentCount);
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}