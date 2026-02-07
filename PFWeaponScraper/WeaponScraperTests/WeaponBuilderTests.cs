using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Services;

namespace PFWeaponScraper.Tests.Services;

[TestClass]
public class WeaponBuilderTests
{
    [TestMethod]
    public void Build_NormalizesInputs_AndCreatesWeapon()
    {
        var builder = new WeaponBuilder();

        var weapon = builder.Build(
            "  Phantom ",
            " Scope\n",
            "None",
            "  none ",
            "Laser\t",
            null
        );

        Assert.AreEqual("Phantom", weapon.GunName);
        Assert.AreEqual(2, weapon.AttachmentCount);

        CollectionAssert.AreEqual(
            new[] { "Scope", "Laser" },
            new System.Collections.Generic.List<string>(weapon.Attachments)
        );
    }

    [TestMethod]
    public void Build_AllNoneAttachments_ResultsInZeroAttachments()
    {
        var builder = new WeaponBuilder();

        var weapon = builder.Build(
            "Vandal",
            "None",
            "none",
            " NONE ",
            null,
            ""
        );

        Assert.AreEqual("Vandal", weapon.GunName);
        Assert.AreEqual(0, weapon.AttachmentCount);
    }
}