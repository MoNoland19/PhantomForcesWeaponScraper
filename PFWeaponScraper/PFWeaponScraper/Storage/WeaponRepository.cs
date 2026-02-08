using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PFWeaponScraper.Models;

namespace PFWeaponScraper.Storage
{
    public class WeaponRepository
    {
        private readonly string _path;
        private readonly List<Weapon> _weapons;

        public IReadOnlyList<Weapon> Weapons => _weapons;

        public WeaponRepository(string path)
        {
            _path = path;

            if (File.Exists(_path))
            {
                _weapons =
                    JsonSerializer.Deserialize<List<Weapon>>(File.ReadAllText(_path))
                    ?? new List<Weapon>();
            }
            else
            {
                _weapons = new List<Weapon>();
            }

            _weapons.Sort();
        }

        public bool AddWeapon(Weapon weapon)
        {
            if (_weapons.Contains(weapon))
                return false;

            _weapons.Add(weapon);
            _weapons.Sort();
            return true;
        }

        public IEnumerable<Weapon> GetByGunName(string gunName)
        {
            return _weapons.Where(w =>
                w.GunName.Equals(gunName, System.StringComparison.OrdinalIgnoreCase));
        }

        public void Save()
        {
            Console.WriteLine($"SAVING COUNT = {_weapons.Count}");
            Console.WriteLine($"SAVING TO PATH = {_path}");

            File.WriteAllText(
                _path,
                JsonSerializer.Serialize(_weapons, new JsonSerializerOptions
                {
                    WriteIndented = true
                })
            );
        }

    }
}