using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Models;
using System.Collections.Generic;

namespace PFWeaponScraper.Tests.Models;

[TestClass]
public class WeaponClassTests
{
    [TestMethod]
    public void Constructor_RemovesNoneAttachments()
    {
        var weapon = new Weapon(
            "Phantom",
            "None",
            "Suppressor",
            "None",
            "Laser",
            "None"
        );

        Assert.AreEqual(2, weapon.AttachmentCount);

        CollectionAssert.AreEqual(
            new List<string> { "Suppressor", "Laser" },
            new List<string>(weapon.Attachments)
        );
    }

    [TestMethod]
    public void AttachmentCount_IsZero_WhenAllAttachmentsAreNone()
    {
        var weapon = new Weapon(
            "Vandal",
            "None",
            "None",
            "None",
            "None",
            "None"
        );

        Assert.AreEqual(0, weapon.AttachmentCount);
        Assert.AreEqual(0, weapon.Attachments.Count);
    }

    [TestMethod]
    public void CompareTo_SortsByGunNameAlphabetically()
    {
        var phantom = new Weapon(
            "Phantom",
            "Suppressor",
            "None",
            "None",
            "None",
            "None"
        );

        var vandal = new Weapon(
            "Vandal",
            "Suppressor",
            "Grip",
            "None",
            "None",
            "None"
        );

        Assert.IsTrue(phantom.CompareTo(vandal) < 0);
    }

    [TestMethod]
    public void CompareTo_SortsByAttachmentCount_WhenGunNamesMatch()
    {
        var fewerAttachments = new Weapon(
            "Phantom",
            "Suppressor",
            "None",
            "None",
            "None",
            "None"
        );

        var moreAttachments = new Weapon(
            "Phantom",
            "Suppressor",
            "Grip",
            "Laser",
            "None",
            "None"
        );

        Assert.IsTrue(moreAttachments.CompareTo(fewerAttachments) < 0);
    }

    [TestMethod]
    public void ListSort_OrdersWeaponsCorrectly()
    {
        var weapons = new List<Weapon>
        {
            new Weapon("Phantom", "None", "None", "None", "None", "None"),
            new Weapon("Vandal", "Suppressor", "None", "None", "None", "None"),
            new Weapon("Phantom", "Suppressor", "Grip", "None", "None", "None"),
            new Weapon("Phantom", "Suppressor", "None", "None", "None", "None")
        };

        weapons.Sort();

        Assert.AreEqual("Phantom", weapons[0].GunName);
        Assert.AreEqual(2, weapons[0].AttachmentCount);

        Assert.AreEqual("Phantom", weapons[1].GunName);
        Assert.AreEqual(1, weapons[1].AttachmentCount);

        Assert.AreEqual("Phantom", weapons[2].GunName);
        Assert.AreEqual(0, weapons[2].AttachmentCount);

        Assert.AreEqual("Vandal", weapons[3].GunName);
    }

    [TestMethod]
    public void ToJson_DoesNotIncludeNone_AndHasCorrectStructure()
    {
        var weapon = new Weapon(
            "Phantom",
            "Scope",
            "Suppressor",
            "None",
            "Laser",
            "None"
        );

        string json = weapon.ToJson();

        StringAssert.Contains(json, "\"gun\"");
        StringAssert.Contains(json, "\"Phantom\"");
        StringAssert.Contains(json, "\"attachments\"");
        StringAssert.Contains(json, "Scope");
        StringAssert.Contains(json, "Suppressor");
        StringAssert.Contains(json, "Laser");
        Assert.IsFalse(json.Contains("None"));
    }
}
