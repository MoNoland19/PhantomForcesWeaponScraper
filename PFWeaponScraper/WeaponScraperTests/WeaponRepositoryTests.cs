using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Models;
using PFWeaponScraper.Storage;
using System;
using System.IO;
using System.Linq;

namespace PFWeaponScraper.Tests
{
    [TestClass]
    public class WeaponRepositoryTests
    {
        private string _testPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "weaponsTest.json"
            );

        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(_testPath))
                File.Delete(_testPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testPath))
                File.Delete(_testPath);
        }


        [TestMethod]
        public void Constructor_FileDoesNotExist_StartsEmpty()
        {
            var repo = new WeaponRepository(_testPath);

            Assert.AreEqual(0, repo.Weapons.Count);
        }

        [TestMethod]
        public void AddWeapon_NewWeapon_IsAdded()
        {
            var repo = new WeaponRepository(_testPath);

            var weapon = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "",
                "",
                ""
            );

            bool added = repo.AddWeapon(weapon);

            Assert.IsTrue(added);
            Assert.AreEqual(1, repo.Weapons.Count);
        }

        [TestMethod]
        public void AddWeapon_DuplicateVariant_IsNotAdded()
        {
            var repo = new WeaponRepository(_testPath);

            var weapon1 = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "",
                "",
                ""
            );

            var weapon2 = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "",
                "",
                ""
            );

            repo.AddWeapon(weapon1);
            bool added = repo.AddWeapon(weapon2);

            Assert.IsFalse(added);
            Assert.AreEqual(1, repo.Weapons.Count);
        }

        [TestMethod]
        public void Save_ThenReload_PersistsWeapons()
        {
            var repo = new WeaponRepository(_testPath);

            var weapon = new Weapon(
                "M4A1",
                "ACOG",
                "",
                "",
                "",
                ""
            );

            repo.AddWeapon(weapon);
            repo.Save();

            var reloadedRepo = new WeaponRepository(_testPath);

            Assert.AreEqual(1, reloadedRepo.Weapons.Count);
            Assert.AreEqual("M4A1", reloadedRepo.Weapons.First().GunName);
        }

        [TestMethod]
        public void Weapons_AreSortedByNameThenAttachmentCount()
        {
            var repo = new WeaponRepository(_testPath);

            repo.AddWeapon(new Weapon(
                "AK47",
                "Reflex",
                "",
                "",
                "",
                ""
            ));

            repo.AddWeapon(new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "Grip",
                "",
                ""
            ));

            var weapons = repo.Weapons.ToList();

            Assert.AreEqual(2, weapons.Count);
            Assert.AreEqual(3, weapons[0].AttachmentCount); // more attachments first
            Assert.AreEqual(1, weapons[1].AttachmentCount);
        }
    }
}
