using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PFWeaponScraper.Models;

namespace PFWeaponScraper.Storage;

public class WeaponRepository
{
    private readonly string _filePath;
    private readonly List<Weapon> _weapons;

    public IReadOnlyList<Weapon> Weapons => _weapons.AsReadOnly();

    public WeaponRepository(string filePath)
    {
        _filePath = filePath;
        _weapons = LoadFromFile();
    }

    /* -------------------- Public API -------------------- */

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        SortWeapons();
        SaveToFile();
    }

    public void Clear()
    {
        _weapons.Clear();
        SaveToFile();
    }

    /* -------------------- Internal Logic -------------------- */

    private List<Weapon> LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return new List<Weapon>();

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Weapon>();

        return JsonSerializer.Deserialize<List<Weapon>>(json)
               ?? new List<Weapon>();
    }

    private void SaveToFile()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_weapons, options);
        File.WriteAllText(_filePath, json);
    }

    private void SortWeapons()
    {
        _weapons.Sort();
    }
}