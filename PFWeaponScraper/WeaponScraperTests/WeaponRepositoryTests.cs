using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Models;
using PFWeaponScraper.Storage;
using System;
using System.IO;
using System.Linq;

namespace PFWeaponScraper.Tests.Storage;

[TestClass]
public class WeaponRepositoryTests
{
    private string _testFilePath = null!;

    [TestInitialize]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"weapon_repo_test_{Guid.NewGuid()}.json"
        );
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [TestMethod]
    public void Constructor_CreatesEmptyRepository_WhenFileDoesNotExist()
    {
        var repo = new WeaponRepository(_testFilePath);

        Assert.AreEqual(0, repo.Weapons.Count);
    }

    [TestMethod]
    public void AddWeapon_AddsWeaponAndPersistsToFile()
    {
        var repo = new WeaponRepository(_testFilePath);

        var weapon = new Weapon(
            "Phantom",
            "Scope",
            "Suppressor",
            "None",
            "Laser",
            "None"
        );

        repo.AddWeapon(weapon);

        Assert.AreEqual(1, repo.Weapons.Count);
        Assert.IsTrue(File.Exists(_testFilePath));

        string json = File.ReadAllText(_testFilePath);
        StringAssert.Contains(json, "Phantom");
    }

    [TestMethod]
    public void Repository_LoadsWeaponsFromExistingFile()
    {
        var repo1 = new WeaponRepository(_testFilePath);

        repo1.AddWeapon(new Weapon(
            "Vandal",
            "None",
            "None",
            "None",
            "None",
            "None"
        ));

        // create a new repository pointing to the same file
        var repo2 = new WeaponRepository(_testFilePath);

        Assert.AreEqual(1, repo2.Weapons.Count);
        Assert.AreEqual("Vandal", repo2.Weapons.First().GunName);
    }

    [TestMethod]
    public void AddWeapon_KeepsWeaponsSorted()
    {
        var repo = new WeaponRepository(_testFilePath);

        repo.AddWeapon(new Weapon(
            "Vandal",
            "Suppressor",
            "None",
            "None",
            "None",
            "None"
        ));

        repo.AddWeapon(new Weapon(
            "Phantom",
            "Suppressor",
            "Grip",
            "None",
            "None",
            "None"
        ));

        repo.AddWeapon(new Weapon(
            "Phantom",
            "None",
            "None",
            "None",
            "None",
            "None"
        ));

        Assert.AreEqual("Phantom", repo.Weapons[0].GunName);
        Assert.AreEqual(2, repo.Weapons[0].AttachmentCount);

        Assert.AreEqual("Phantom", repo.Weapons[1].GunName);
        Assert.AreEqual(0, repo.Weapons[1].AttachmentCount);

        Assert.AreEqual("Vandal", repo.Weapons[2].GunName);
    }

    [TestMethod]
    public void Clear_RemovesAllWeapons_AndClearsFile()
    {
        var repo = new WeaponRepository(_testFilePath);

        repo.AddWeapon(new Weapon(
            "Phantom",
            "Scope",
            "None",
            "None",
            "None",
            "None"
        ));

        repo.Clear();

        Assert.AreEqual(0, repo.Weapons.Count);

        string json = File.ReadAllText(_testFilePath);
        Assert.AreEqual("[]", json.Trim());
    }
}
