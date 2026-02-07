using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Utils;

namespace PFWeaponScraper.Tests.Utils;

[TestClass]
public class TextNormalizerTests
{
    [TestMethod]
    public void Normalize_ReturnsNone_ForNull()
    {
        Assert.AreEqual("None", TextNormalizer.Normalize(null));
    }

    [TestMethod]
    public void Normalize_ReturnsNone_ForWhitespace()
    {
        Assert.AreEqual("None", TextNormalizer.Normalize("   "));
    }

    [TestMethod]
    public void Normalize_TrimsWhitespace()
    {
        Assert.AreEqual("Scope", TextNormalizer.Normalize("  Scope  "));
    }

    [TestMethod]
    public void Normalize_NormalizesNewlines()
    {
        Assert.AreEqual(
            "Red Dot Sight",
            TextNormalizer.Normalize("Red\nDot\tSight")
        );
    }

    [TestMethod]
    public void Normalize_NormalizesNone_CaseInsensitive()
    {
        Assert.AreEqual("None", TextNormalizer.Normalize("none"));
        Assert.AreEqual("None", TextNormalizer.Normalize("NONE"));
        Assert.AreEqual("None", TextNormalizer.Normalize(" NoNe "));
    }

    [TestMethod]
    public void NormalizeGunName_UsesSameLogic()
    {
        Assert.AreEqual(
            "AK-12",
            TextNormalizer.NormalizeGunName("  AK-12 ")
        );
    }

    [TestMethod]
    public void NormalizeAttachment_UsesSameLogic()
    {
        Assert.AreEqual(
            "Suppressor",
            TextNormalizer.NormalizeAttachment("Suppressor\n")
        );
    }
}