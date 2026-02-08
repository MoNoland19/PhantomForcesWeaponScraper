using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace PFWeaponScraper.Input;

public class HotkeyListener : IDisposable
{
    private const int WM_HOTKEY = 0x0312;
    private const int MOD_ALT = 0x0001;

    private readonly Action _action;
    private readonly Thread _loop;
    private bool _running = true;

    public HotkeyListener(Action action)
    {
        _action = action;
        RegisterHotKey(IntPtr.Zero, 1, MOD_ALT, 'L');

        _loop = new Thread(MessageLoop) { IsBackground = true };
        _loop.Start();
    }

    private void MessageLoop()
    {
        while (_running && GetMessage(out MSG msg, IntPtr.Zero, 0, 0))
            if (msg.message == WM_HOTKEY)
                _action();
    }

    public void Dispose()
    {
        _running = false;
        UnregisterHotKey(IntPtr.Zero, 1);
    }

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int mod, int key);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll")]
    private static extern bool GetMessage(out MSG msg, IntPtr hWnd, uint min, uint max);

    private struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public int x, y;
    }
}