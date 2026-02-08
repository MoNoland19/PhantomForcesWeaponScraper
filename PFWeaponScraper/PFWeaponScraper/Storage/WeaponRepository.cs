using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PFWeaponScraper.Models;

namespace PFWeaponScraper.Storage;

public class WeaponRepository
{
    private readonly string _path;
    private readonly List<Weapon> _weapons;

    public IReadOnlyList<Weapon> Weapons => _weapons;

    public WeaponRepository(string path)
    {
        _path = path;
        _weapons = File.Exists(path)
            ? JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText(path)) ?? []
            : [];
    }

    public void AddWeapon(Weapon weapon)
    {
        if (_weapons.Any(w =>
                w.GunName == weapon.GunName &&
                w.Attachments.SequenceEqual(weapon.Attachments)))
            return;

        _weapons.Add(weapon);
        _weapons.Sort();
    }

    public void Save()
    {
        File.WriteAllText(_path,
            JsonSerializer.Serialize(_weapons, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
    }
}