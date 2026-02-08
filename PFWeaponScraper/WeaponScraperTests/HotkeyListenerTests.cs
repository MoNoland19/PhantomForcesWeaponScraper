using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFWeaponScraper;
using System.Threading;
using PFWeaponScraper.Input;

namespace PFWeaponScraper.Tests
{
    [TestClass]
    public class HotkeyListenerTests
    {
        [TestMethod]
        public void Start_DoesNotThrow()
        {
            var listener = new HotkeyListener();

            bool fired = false;
            listener.OnLPressed += () => fired = true;

            listener.Start();

            // Let thread spin briefly
            Thread.Sleep(50);

            // We cannot trigger the key in unit tests
            Assert.IsFalse(fired);
        }
    }
}