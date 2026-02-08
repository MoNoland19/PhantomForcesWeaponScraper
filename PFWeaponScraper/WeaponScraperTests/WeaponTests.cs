using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper.Models;
using System.Linq;

namespace PFWeaponScraper.Tests
{
    [TestClass]
    public class WeaponTests
    {
        [TestMethod]
        public void Constructor_NormalizesGunName()
        {
            var weapon = new Weapon(
                "  AK47  ",
                "Reflex",
                "",
                "",
                "",
                ""
            );

            Assert.AreEqual("AK47", weapon.GunName);
        }

        [TestMethod]
        public void Constructor_RemovesEmptyAndNoneAttachments()
        {
            var weapon = new Weapon(
                "AK47",
                "Reflex",
                "None",
                "",
                "  ",
                "AP"
            );

            CollectionAssert.AreEqual(
                new[] { "AP", "Reflex" },
                weapon.Attachments.ToList()
            );
        }

        [TestMethod]
        public void AttachmentCount_ReturnsCorrectCount()
        {
            var weapon = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "",
                "",
                ""
            );

            Assert.AreEqual(2, weapon.AttachmentCount);
        }

        [TestMethod]
        public void Equals_SameGunAndAttachments_AreEqual()
        {
            var w1 = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "",
                "",
                ""
            );

            var w2 = new Weapon(
                "ak47",
                "Compensator",
                "Reflex",
                "",
                "",
                ""
            );

            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void Equals_DifferentAttachments_AreNotEqual()
        {
            var w1 = new Weapon(
                "AK47",
                "Reflex",
                "",
                "",
                "",
                ""
            );

            var w2 = new Weapon(
                "AK47",
                "ACOG",
                "",
                "",
                "",
                ""
            );

            Assert.IsFalse(w1.Equals(w2));
        }

        [TestMethod]
        public void CompareTo_SortsByGunNameThenAttachmentCount()
        {
            var fewerAttachments = new Weapon(
                "AK47",
                "Reflex",
                "",
                "",
                "",
                ""
            );

            var moreAttachments = new Weapon(
                "AK47",
                "Reflex",
                "Compensator",
                "Grip",
                "",
                ""
            );

            Assert.IsTrue(moreAttachments.CompareTo(fewerAttachments) < 0);
        }
    }
}
