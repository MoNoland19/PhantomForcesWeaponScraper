using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace PFWeaponScraper.Input
{
    public class HotkeyListener
    {
        public event Action? OnLPressed;

        private bool _running;

        public void Start()
        {
            _running = true;

            Thread thread = new Thread(Listen)
            {
                IsBackground = true
            };

            thread.Start();
        }

        private void Listen()
        {
            while (_running)
            {
                if (GetAsyncKeyState(0x4C) < 0) // L key
                {
                    OnLPressed?.Invoke();
                    Thread.Sleep(500); // debounce
                }

                if (GetAsyncKeyState(0x1B) < 0) // ESC
                {
                    _running = false;
                }

                Thread.Sleep(10);
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
    }
}